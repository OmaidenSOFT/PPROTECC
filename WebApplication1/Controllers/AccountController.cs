// WebApplication1.Controllers.AccountController
using System;
using System.Configuration;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
using LogicBo;
using WebApplication1.Models;

public class AccountController : Controller
{
	private readonly AccountBo _accountBo = new AccountBo();
	WorkingAtHeightBo _workingAtHeightBo = new WorkingAtHeightBo();
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
			base.Session["CountryID"] = ((collection["cbxCountry"].ToString() != string.Empty) ? Convert.ToInt32(collection["cbxCountry"].ToString()) : 0);
			string pais = _workingAtHeightBo.GetCountryNameByID(((collection["cbxCountry"].ToString() != string.Empty) ? Convert.ToInt32(collection["cbxCountry"].ToString()) : 0));
			string bandera= _workingAtHeightBo.GetFlagNameByID(((collection["cbxCountry"].ToString() != string.Empty) ? Convert.ToInt32(collection["cbxCountry"].ToString()) : 0));

			string lang = (collection["cbxlanguage"].ToString() != string.Empty) ? collection["cbxlanguage"].ToString() : "";
			string mess = string.Empty;
			string Welcome= string.Empty;
			string invite= string.Empty;
			string textopp1=string.Empty;
			string textopp2 = string.Empty;
			string textopp3 = string.Empty;
			string textopp4 = string.Empty;

			if (lang == "english")
            {
                mess = "Process completed successfully";
				Welcome = "WELCOME TO THE PPROTECC PORTAL FOR  " + pais;
				invite = "WE INVITE YOU TO BE PART OF THIS EXPERIENCE!";

				textopp1 = "PPROTECC, is a software designed to guarantee the real - time administration of the program for work at heights of the ENEL organization, in this you will find";
				textopp2 = "different modules that will guide you in the management of the program, for each of the organization's headquarters.";
				textopp3 = "PPROTECC was conceived as a management tool for the prevention of work accidents at heights, to strengthen the application of organizational policies";
				textopp4 = "and ensure timely compliance with current legislation and regulations.";
			}
            else if (lang == "spain")
            {
                mess = "Proceso completado con éxito";
				Welcome = "BIENVENIDO AL PORTAL PPROTECC PARA " + pais;
				invite = "¡TE INVITAMOS A HACER PARTE DE ESTA EXPERIENCIA!";
				textopp1 = "PPROTECC,  es un software diseñado para garantizar la administración en tiempo real del programa para trabajo en alturas de la organización ENEL,  en este encontraras";
				textopp2 = "diferentes módulos que te guiaran en la gestión del programa, para cada una de las sedes de la organización";
				textopp3 = "PPROTECC fue pensado como herramienta de gestión para la prevención de accidentes de trabajo en alturas, para fortalecer la aplicación de las políticas organizacionales  y";
				textopp4 = "velar por el cumplimiento oportuno de la  legislación y normatividad vigente.";
			}
			else if (lang == "italiano")
            {
                mess = "Processo completato con successo";
				Welcome ="BENVENUTO NEL PORTALE PPROTECC PER LA " + pais;
				invite = "VI INVITIAMO A FAR PARTE DI QUESTA ESPERIENZA";
				textopp1 = "PPROTECC, è un software pensato per garantire l'amministrazione in tempo reale del programma per i lavori in quota dell'organizzazione ENEL, in esso troverete";
				textopp2 = "diversi moduli che ti guideranno nella gestione del programma, per ciascuna sede dell'organizzazione.";
				textopp3 = "PPROTECC nasce come strumento gestionale per la prevenzione degli infortuni sul lavoro in quota, per rafforzare l'applicazione";
				textopp4 = "delle politiche organizzative e garantire il tempestivo rispetto della normativa e dei regolamenti vigenti.";
			}


			if (sessionModels != null)
			{
				sessionModels.IdUser = userEntityAccount.IdUser;
				sessionModels.NameUser = userEntityAccount.NameUser + " " + userEntityAccount.SurNameUser;
				sessionModels.SedeCategoriaId = userEntityAccount.SedeCategoriaId;
				sessionModels.LoginUser = userEntityAccount.LoginUser;
				sessionModels.Pais=pais;
				sessionModels.Bandera = bandera;
				sessionModels.Welcome = Welcome;
				sessionModels.Invite = invite;
				sessionModels.TextoPro1 = textopp1;
				sessionModels.TextoPro2 = textopp2;
				sessionModels.TextoPro3 = textopp3;
				sessionModels.TextoPro4 = textopp4;


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
                    RolId = userEntityAccount.RolId,
                    Pais = pais,
					Bandera=bandera,
					Welcome=Welcome,
					Invite=invite,
					TextoPro1=textopp1,
					TextoPro2=textopp2,
					TextoPro3=textopp3,
					TextoPro4=textopp4,
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


			




			return Json(new { result = true, message = mess, url = Url.Action("Index", "Home") }, JsonRequestBehavior.AllowGet);
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
