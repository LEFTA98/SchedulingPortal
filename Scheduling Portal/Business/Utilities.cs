using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.IO;			//for FileStream, MemoryStream

namespace RotmanTrading.Business
{
    /// <summary>
    /// Developed by ESP Technologies (www.ESPTech.com).
    /// </summary>
    public class Utilities
    {
        public const string ENCRYPTION_KEY = "&%#@?,:*";
        private static string m_strWebAppNameAndVersion = string.Empty;

        public static string WebApplicationNameAndVersion
        {
            set
            {
                m_strWebAppNameAndVersion = value;
            }
            get
            {
                return m_strWebAppNameAndVersion;
            }
        }

        public static string FromEmailAddr()
        {
            return ConfigurationManager.AppSettings["EmailFrom"];
        }

        public static string ToEmailAddrForErrors()
        {
            return ConfigurationManager.AppSettings["ToEmailAddrForErrors"];
        }

        public static bool SendEmailNotification(string strTo, string strSubj, string strBody, ArrayList errorList)
        {
            try
            {
                SendEMail(FromEmailAddr(), strTo, strSubj, strBody, MailPriority.High, "", "", true);
                return true;
            }
            catch (Exception ex)
            {
                errorList.Add("There was a problem sending the email");
                string strError;
                try
                {
                    strError = "There was a problem sending the email ";
                    strError += "Error Description < " + ex.Message + "> ";
                    strError += "Error Source < " + ex.Source + "> ";
                    strError += "Function < cBasePage.SendEmailNotification > ";
                    strError += "To < " + strTo + " > ";
                    strError += "Message < " + strBody + " > ";
                    return false;
                }
                catch
                {
                    return false;
                }	//end try/catch
            }	//end try/catch
        }

        public static void CriticalErrorNotification(string strError, bool bSendEmail)
        {
            try
            {
                //send email to web master
                if (bSendEmail)
                {
                    System.Collections.ArrayList errorList = new System.Collections.ArrayList();
                    strError = string.Format("{0} ... Server: [{1}]", strError, System.Net.Dns.GetHostName());
                    string strSubject = string.Format("{0} Critical Error Notification", WebApplicationNameAndVersion);
                    SendEmailNotification(ToEmailAddrForErrors(), strSubject, strError, errorList);
                    errorList = null;
                }	//if bSendEmail
            }
            catch //(Exception ex)
            {
            }
        }	//end CriticalErrorNotification

        public static void JustInfoNotification(string strError, bool bSendEmail)
        {
            try
            {
                //send email to web master
                if (bSendEmail)
                {
                    System.Collections.ArrayList errorList = new System.Collections.ArrayList();
                    strError = string.Format("{0} ... Server: [{1}]", strError, System.Net.Dns.GetHostName());
                    string strSubject = string.Format("{0} Info Notification", WebApplicationNameAndVersion);
                    SendEmailNotification(ToEmailAddrForErrors(), strSubject, strError, errorList);
                    errorList = null;
                }	//if bSendEmail
            }
            catch //(Exception ex)
            {
            }
        }	//end CriticalErrorNotification

        public static bool SendEMail(
            string from, 
            string recipient, 
            string subj, 
            string body, 
            MailPriority importance, 
            string cc, 
            string bcc, 
            bool bUseHtml)
        {

            //validate input data
            bool bAllDataValid = true;
            if (from.Length == 0)
                bAllDataValid = false;
            else if (recipient.Length == 0)
                bAllDataValid = false;
            else if (subj.Length == 0)
                bAllDataValid = false;
            else if (body.Length == 0)
                bAllDataValid = false;

            if (bAllDataValid)
            {
                string strSMTPServer = ConfigurationManager.AppSettings["SMTPServer"];

                if (strSMTPServer == null)
                {
                    return false;
                }
                if (strSMTPServer.Length == 0)
                {
                    return false;
                }

                MailAddress objFrom = new MailAddress(from);
                MailAddress objTo = new MailAddress(recipient);
                MailMessage objMsg = new MailMessage(objFrom, objTo);
                objMsg.Subject = subj;
                objMsg.Body = body;
                objMsg.IsBodyHtml = bUseHtml;
                objMsg.Priority = MailPriority.Normal;
                if (string.IsNullOrEmpty(cc) == false)
                {
                    objMsg.CC.Add(new MailAddress(cc));
                }
                if (string.IsNullOrEmpty(bcc) == false)
                {
                    objMsg.Bcc.Add(new MailAddress(bcc));
                }

                SmtpClient objClient = new SmtpClient(strSMTPServer);
                //Credentials are necessary if the server requires the client 
                //to authenticate before it will send e-mail on the client's behalf.
                //objClient.Credentials = CredentialCache.DefaultNetworkCredentials
                //objClient.Send(objMsg);

                string strSMTPLoginName = ConfigurationManager.AppSettings["SMTPLoginName"];
                string strSMTPLoginPassword = ConfigurationManager.AppSettings["SMTPLoginPassword"];
                if (!string.IsNullOrEmpty(strSMTPLoginName) && !string.IsNullOrEmpty(strSMTPLoginPassword))
                {
                    System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential(strSMTPLoginName, strSMTPLoginPassword);
                    objClient.UseDefaultCredentials = false;
                    objClient.Credentials = basicAuthenticationInfo;
                }
                objClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objClient.Send(objMsg);

                return true;

            }	//if bAllDataValid
            else
                return false;
        }	//end function SendEMail

        public string EncryptData(string strText)
        {
            byte[] byKey;
            byte[] IV = new byte[8];
            DESCryptoServiceProvider des;
            MemoryStream objMS;
            CryptoStream objCS;
            byte[] arrInputByte = new byte[strText.Length];
            string strEncrKey = ENCRYPTION_KEY;

            try
            {
                IV[0] = 10;
                IV[1] = 20;
                IV[2] = 30;
                IV[3] = 40;
                IV[4] = 50;
                IV[5] = 60;
                IV[6] = 70;
                IV[7] = 80;

                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                des = new DESCryptoServiceProvider();
                arrInputByte = System.Text.Encoding.UTF8.GetBytes(strText);
                objMS = new System.IO.MemoryStream();
                objCS = new CryptoStream(objMS, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                objCS.Write(arrInputByte, 0, arrInputByte.Length);
                objCS.FlushFinalBlock();
                objCS.Close();
                return Convert.ToBase64String(objMS.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Encrypt Data error: [{0}]", ex.Message));
            }
            finally
            {
                des = null;
                objMS = null;
                objCS = null;
            }
        }

        public string DecryptData(string strEncryptedText)
        {
            byte[] byKey;
            byte[] IV = new byte[8];
            DESCryptoServiceProvider des;
            MemoryStream objMS;
            CryptoStream objCS;
            byte[] arrInputByte = new byte[strEncryptedText.Length];
            string strDecrKey = ENCRYPTION_KEY;
            System.Text.Encoding objEncode = System.Text.Encoding.UTF8;

            try
            {
                IV[0] = 10;
                IV[1] = 20;
                IV[2] = 30;
                IV[3] = 40;
                IV[4] = 50;
                IV[5] = 60;
                IV[6] = 70;
                IV[7] = 80;

                byKey = System.Text.Encoding.UTF8.GetBytes(strDecrKey.Substring(0, 8));
                des = new DESCryptoServiceProvider();
                arrInputByte = Convert.FromBase64String(strEncryptedText);
                objMS = new System.IO.MemoryStream();
                objCS = new CryptoStream(objMS, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                objCS.Write(arrInputByte, 0, arrInputByte.Length);
                objCS.FlushFinalBlock();
                objCS.Close();
                return objEncode.GetString(objMS.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Decrypt Data error: [{0}]", ex.Message));
            }
            finally
            {
                des = null;
                objMS = null;
                objCS = null;
            }
        }

    }   //public class Utilities
}
