// WebApplication1.Controllers.AccountController
using LogicBo;
using System;
using System.Configuration;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;

public class AccountController : Controller
{
    private readonly AccountBo _accountBo = new AccountBo();

    private int sedecategoriaid;

    private string email;

    [AllowAnonymous]
    public ActionResult Login(string returnUrl)
    {
        base.ViewBag.ReturnUrl = returnUrl;
        base.ViewBag.CountryDictionary = new SelectList(_accountBo.GetDictionary(), "Key", "Value");
        return View();
    }

    public ActionResult LogOff()
    {
        base.Session["SessionUser"] = null;
        FormsAuthentication.SignOut();
        return RedirectToAction("Login");
    }

    public void SendMail(string Message, DataTable dsContactos)
    {
        try
        {
            string text = ConfigurationManager.AppSettings["servicioalcliente"].ToString();
            string password = ConfigurationManager.AppSettings["Passservicioalcliente"].ToString();
            foreach (DataRow row in dsContactos.Rows)
            {
                MailMessage mailMessage = new MailMessage(new MailAddress(text), new MailAddress(row["Email"].ToString()))
                {
                    Subject = "Alerta de vencimiento de Certificado de aptitud"
                };
                string body = "<br /><font size='4' color='red'><b> ¡Atención! </b></font><br />  <h4><b>" + Message + "</b></h4>";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                mailMessage.From = new MailAddress(text);
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["gerentegralmail"].ToString()))
                {
                    MailAddress item = new MailAddress(ConfigurationManager.AppSettings["gerentegralmail"].ToString());
                    mailMessage.CC.Add(item);
                }
                MailAddress item2 = new MailAddress(text);
                mailMessage.CC.Add(item2);
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = ConfigurationManager.AppSettings["fromsmtp"].ToString();
                smtpClient.Port = Convert.ToInt16(ConfigurationManager.AppSettings["PortSmtp"].ToString());
                smtpClient.Credentials = new NetworkCredential(text, password);
                smtpClient.Send(mailMessage);
            }
        }
        catch (Exception)
        {
        }
    }

    [HttpPost]
    public JsonResult ValidateLogin(FormCollection collection)
    {
        try
        {
            AccountBo.UserEntityAccount userEntityAccount = _accountBo.ValidUser(collection["nameUser"], collection["password"]);
            if (string.IsNullOrEmpty(userEntityAccount.LoginUser))
            {
                throw new Exception("No se han encontrado datos de este usuario");
            }
            SessionModels sessionModels = base.Session["SessionUser"] as SessionModels;
            if (sessionModels != null)
            {
                sessionModels.IdUser = userEntityAccount.IdUser;
                sessionModels.NameUser = userEntityAccount.NameUser + " " + userEntityAccount.SurNameUser;
                sessionModels.SedeCategoriaId = userEntityAccount.SedeCategoriaId;
                sessionModels.LoginUser = userEntityAccount.LoginUser;
                base.Session["SessionUser"] = sessionModels;
            }
            else
            {
                SessionModels sessionModels2 = new SessionModels
                {
                    IdUser = userEntityAccount.IdUser,
                    NameUser = userEntityAccount.NameUser + " " + userEntityAccount.SurNameUser,
                    LoginUser = userEntityAccount.LoginUser,
                    SedeCategoriaId = userEntityAccount.SedeCategoriaId,
                    RolId = userEntityAccount.RolId
                };
                base.Session["SessionUser"] = sessionModels2;
                sedecategoriaid = Convert.ToInt32(sessionModels2.SedeCategoriaId);
                email = sessionModels2.Email;
                base.Session["CountryID"] = ((collection["cbxCountry"].ToString() != string.Empty) ? Convert.ToInt32(collection["cbxCountry"].ToString()) : 0);
            }
            DataSet dataSet = _accountBo.CreateEmailAptitudConcept();
            foreach (DataRow row in dataSet.Tables[1].Rows)
            {
                if (Convert.ToDateTime(row["fechaenvio"]).Year != 1900)
                {
                    SendMail(row["Con"].ToString(), dataSet.Tables[0]);
                    _accountBo.CreateSentEmailt(Convert.ToInt32(row["Id"]));
                }
                else
                {
                    SendMail(row["Con"].ToString(), dataSet.Tables[0]);
                    _accountBo.CreateSentEmailt(Convert.ToInt32(row["Id"]));
                }
            }
            foreach (DataRow row2 in dataSet.Tables[2].Rows)
            {
                if (Convert.ToDateTime(row2["fechaenvio"]).Year != 1900)
                {
                    SendMail(row2["Con"].ToString(), dataSet.Tables[0]);
                    _accountBo.CreateSentEmailt(Convert.ToInt32(row2["Id"]));
                }
                else
                {
                    SendMail(row2["Con"].ToString(), dataSet.Tables[0]);
                    _accountBo.CreateSentEmailt(Convert.ToInt32(row2["Id"]));
                }
            }
            return Json(new { result = true, message = "Proceso completado con éxito", url = Url.Action("Index", "Home") }, JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            Exception ex2 = ex;
            return Json(new
            {
                result = false,
                message = ex2.Message
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
