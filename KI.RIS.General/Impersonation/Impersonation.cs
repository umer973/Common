using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Configuration;
//using Infologics.Medilogics.General.EncryptionDecryption.Algorithms;
//using Infologics.Medilogics.General.EncryptionDecryption.Keys;
//using Infologics.Medilogics.General.EncryptionDecryption;
using System.Security.Permissions;


/*-----------------------------------------------------------------------
<Copyright file="ImpersonateTasks.cs" Company="Kameda Infologics">
    Copyright@Kameda Infologics Pvt Ltd. All rights reserved.
</Copyright>

 Description     :To execute a taks with a different credential..ie impersonation .Implemented in  3.1 version(file access area)
 Created  By     :Sreeroop 
 Created  Date   :17-07-2018
 Modified By     :ModifiedBy  
 Modified Date   :ModifiedDate 
 Modified Purpose:ModifiedPur 
 -----------------------------------------------------------------------*/


namespace KI.RIS.General.Impersonation
{
	/// <summary>
	/// To impersonate two tier and wcf level (file accessing etc)
	/// </summary>
	public class Impersonation : IDisposable
	{
		[DllImport("advapi32.DLL", SetLastError = true)]
		public static extern int LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public extern static bool CloseHandle(IntPtr handle);

		WindowsImpersonationContext m_impersonatedUser = null;
		IntPtr m_tokenHandle = default(IntPtr);

		# region Dispose
		~Impersonation()
		{
			Dispose(false);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Revert to original user identity
				if (m_impersonatedUser != null)
					m_impersonatedUser.Undo();
			}

			// Free the tokens.
			if (m_tokenHandle != IntPtr.Zero)
				CloseHandle(m_tokenHandle);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

        #endregion

        public bool GetImpersionation()
        {
            bool bReturn = true;

            string sUserName = ConfigurationManager.AppSettings.Get("ImperUserName");
            string sPassword = ConfigurationManager.AppSettings.Get("ImperPassword");
            string sDomain = ConfigurationManager.AppSettings.Get("ImperDomain");

            bReturn = LogonUser(bReturn, sUserName, sDomain, sPassword);

            return bReturn;
        }

		/// <summary>
		/// To handle the impersonation in two tier level and wcf wise(Now implemented in file accessing area(By Sreeroop)
		/// </summary>
		/// <returns></returns>
		public bool GetImpersonationAllMode(string UserName, string Domain, string PWDFileName)
		{
			bool bReturn = true;
			/////Sreeroop....To handle the shared file with impersonation..with a specific user.
			bReturn = LogonUser(bReturn, UserName, Domain, PWDFileName);
			return bReturn;
		}
		[PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
		private bool LogonUser(bool bReturn, string UserName, string Domain, string PWDFileName)
		{
			bReturn = false;
			WindowsIdentity wid_current = WindowsIdentity.GetCurrent();
			WindowsIdentity wid_admin = null;

			string sUserName = UserName;// ConfigurationManager.AppSettings.Get("ImperUserName");
			string sDomain = Domain;// ConfigurationManager.AppSettings.Get("ImperDomain");

			//Commented by vasim as per the instruction of sreeroop - change password file location to pswd
            //string sPassword = PasswordHelper.GetSharedFileAccessPassword(PWDFileName);
			string sPassword = PWDFileName;
            

			//TripleDES oTrpleDes = new TripleDES();
			//sPassword = oTrpleDes.Decrypt(sPassword, Infologics.Medilogics.General.EncryptionDecryption.Keys.FluenceKey.GetKey());
			try
			{
				const int LOGON32_PROVIDER_DEFAULT = 0;
				// create token
				const int LOGON32_LOGON_INTERACTIVE = 2;
				//const int SecurityImpersonation = 2;

				const int LOGON32_LOGON_NEW_CREDENTIALS = 9;

				int Impersonated = 0;
				// get handle to token
				try
				{
					string strDomainName = System.DirectoryServices.ActiveDirectory.Domain.GetComputerDomain().Name;
					if (!string.IsNullOrEmpty(strDomainName) && strDomainName.Length > 0 && strDomainName==sDomain)
					{
						Impersonated = LogonUser(sUserName, sDomain, sPassword, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref m_tokenHandle);
					}
					else //if (Impersonated == 0) /// work group 
					{
						Impersonated = LogonUser(sUserName, sDomain, sPassword, LOGON32_LOGON_NEW_CREDENTIALS, LOGON32_PROVIDER_DEFAULT, ref m_tokenHandle);
					}
				}
				catch
				{
					//if (Impersonated == 0) 
					Impersonated = LogonUser(sUserName, sDomain, sPassword, LOGON32_LOGON_NEW_CREDENTIALS, LOGON32_PROVIDER_DEFAULT, ref m_tokenHandle);
				}

				//sPassword = "TEST@KAMEDA";
                if (Impersonated != 0)
                {
                    wid_admin = new WindowsIdentity(m_tokenHandle);
                    m_impersonatedUser = wid_admin.Impersonate();
                    bReturn = true;
                }
                else
                {
                    throw new KI.RIS.General.RisException.RisException("File access permission denied....");
                }
            }
            catch (Exception)
            {
                bReturn = false;
                throw;
            }
            return bReturn;
        }
    }
}
