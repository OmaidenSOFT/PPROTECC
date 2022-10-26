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
    private string descripciones;
    private string piedepaginacards;

    [AllowAnonymous]
	public ActionResult Login(string returnUrl)
	{
		base.ViewBag.ReturnUrl = returnUrl;
		base.ViewBag.CountryDictionary = new SelectList(_accountBo.GetDictionary(), "Key", "Value");
		return View();
	}

	public ActionResult LoginFacade(string returnUrl)
	{
		base.ViewBag.ReturnUrl = returnUrl;
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
			string modulos = string.Empty;
			string subtitulos = string.Empty;
			if (lang == "english")
            {
                mess = "Process completed successfully";
				Welcome = "WELCOME TO THE PPROTECC 2.0 PORTAL";
				invite = "WE INVITE YOU TO BE PART OF THIS EXPERIENCE!";

				textopp1 = "PPROTECC, is a software designed to guarantee the real - time administration of the program for work at heights of the ENEL organization, in this you will find";
				textopp2 = "different modules that will guide you in the management of the program, for each of the organization's headquarters.";
				textopp3 = "PPROTECC was conceived as a management tool for the prevention of work accidents at heights, to strengthen the application of organizational policies";
				textopp4 = "and ensure timely compliance with current legislation and regulations.";
				modulos = "Working at Height|Lifting of loads|Confined spaces|Electric risk|Fire extinguishers management";
				subtitulos = "The Best Way To Manage A Risk Is To Know About It|Is Lifting Loads The Most Important Process?|COMING SOON|COMING SOON|COMING SOON";
				descripciones = "Work at height is work that is carried out in any place where, if the necessary precautions have not been taken, a person can fall from a height that can cause injury (a fall through a fragile roof, down an unprotected elevator shaft down a stairwell).|First, what is a lift? We can understand what a hoist is as a way of lifting or moving objects with the help of some devices, which is done in a safe, controlled and well calculated way.|A confined space or confined space is one that has reduced entry openings, unfavorable natural ventilation and is not designed to remain inside. Therefore, it can present an unbreathable atmosphere and harbor toxic or flammable gases, vapors or particles.|Electrical risk is considered when there is a possibility of contact of the human body with the electric current and that can be a danger to the integrity of people.|More specifically, a fire extinguisher could be defined as an autonomous device, designed as a cylinder, which can be moved by a single person and which, using a drive mechanism under gas pressure or mechanical pressure, launches an extinguishing agent towards the base. of the fire, in order to extinguish it.";
				
				piedepaginacards = "Last inspections|6 months ago";
				
			}
            else if (lang == "spain")
            {
				piedepaginacards = "Ultimas inspecciones|Hace 6 meses";
				mess = "Proceso completado con éxito";
				Welcome = "BIENVENIDO AL PORTAL PPROTECC 2.0";
				invite = "¡TE INVITAMOS A HACER PARTE DE ESTA EXPERIENCIA!";
				textopp1 = "PPROTECC,  es un software diseñado para garantizar la administración en tiempo real del programa para trabajo en alturas de la organización ENEL,  en este encontraras";
				textopp2 = "diferentes módulos que te guiaran en la gestión del programa, para cada una de las sedes de la organización";
				textopp3 = "PPROTECC fue pensado como herramienta de gestión para la prevención de accidentes de trabajo en alturas, para fortalecer la aplicación de las políticas organizacionales  y";
				textopp4 = "velar por el cumplimiento oportuno de la  legislación y normatividad vigente.";
				modulos = "Trabajo en alturas|Izage de Cargas|Espacios confinados|Riesgo eléctrico|Manejo Extintores";
				subtitulos = "La Mejor Forma De Administrar Un Riesgo Es Conocerlo|¿Es El Izaje De Cargas El Proceso Más Importante?|PROXIMAMENTE|PROXIMAMENTE|PROXIMAMENTE";
				descripciones = "Trabajo en altura es aquel que se realiza en cualquier lugar donde, si no se han adoptado las precauciones necesarias, una persona puede caer desde una altura que puede provocar lesiones (una caída a través de un tejado frágil, por un foso de ascensor sin protección, por el hueco de una escalera).|Primero, ¿qué es un izaje? Podemos entender lo que es un izaje como una forma de levantar o mover objetos con ayuda de algunos dispositivos, el cual se hace de una forma segura, controlada y bien calculada.|Un espacio confinado o recinto confinado es aquel que dispone de aberturas de entrada reducidas, una ventilación natural desfavorable y no está concebido para permanecer en su interior. Por ello, puede presentar una atmósfera irrespirable y albergar gases, vapores o partículas tóxicas o inflamables.|Se considera riesgo eléctrico cuándo existe una posibilidad de contacto del cuerpo humano con la corriente eléctrica y que puede resultar un peligro para la integridad de las personas.|De forma más concreta se podría definir un extintor como un aparato autónomo, diseñado como un cilindro, que puede ser desplazado por una sola persona y que usando un mecanismo de impulsión bajo presión de un gas o presión mecánica, lanza un agente extintor hacia la base del fuego, para lograr extinguirlo.";
			}
			else if (lang == "italiano")
            {
                mess = "Processo completato con successo";
				Welcome ="BENVENUTO NEL PORTALE PPROTECC 2.0";
				invite = "VI INVITIAMO A FAR PARTE DI QUESTA ESPERIENZA";
				textopp1 = "PPROTECC, è un software pensato per garantire l'amministrazione in tempo reale del programma per i lavori in quota dell'organizzazione ENEL, in esso troverete";
				textopp2 = "diversi moduli che ti guideranno nella gestione del programma, per ciascuna sede dell'organizzazione.";
				textopp3 = "PPROTECC nasce come strumento gestionale per la prevenzione degli infortuni sul lavoro in quota, per rafforzare l'applicazione";
				textopp4 = "delle politiche organizzative e garantire il tempestivo rispetto della normativa e dei regolamenti vigenti.";
				modulos = "Lavori in quota|Sollevamento carichi|Spazi confinati|Rischio elettrico|Gestione estintori";
				subtitulos = "Il modo migliore per gestire un rischio è conoscerlo|Il sollevamento di carichi è il processo più importante?|PROSSIMAMENTE|PROSSIMAMENTE|PROSSIMAMENTE";
				descripciones = "I lavori in quota sono lavori che vengono eseguiti in qualsiasi luogo in cui, se non sono state prese le precauzioni necessarie, una persona può cadere da un'altezza che può causare lesioni (caduta da un tetto fragile, da un vano ascensore non protetto giù da una tromba delle scale ).|Innanzitutto, cos'è un ascensore? Possiamo capire cos'è un paranco come mezzo per sollevare o spostare oggetti con l'ausilio di alcuni dispositivi, cosa che avviene in modo sicuro, controllato e ben calcolato.|Uno spazio confinato o spazio confinato è uno che ha aperture di ingresso ridotte, ventilazione naturale sfavorevole e non è progettato per rimanere all'interno. Pertanto, può presentare un'atmosfera irrespirabile e ospitare gas, vapori o particelle tossici o infiammabili.|Il rischio elettrico è considerato quando esiste la possibilità di contatto del corpo umano con la corrente elettrica e ciò può rappresentare un pericolo per l'integrità delle persone.|Più precisamente, un estintore potrebbe essere definito come un dispositivo autonomo, concepito come una bombola, che può essere spostata da una sola persona e che, tramite un meccanismo di azionamento a pressione di gas o meccanica, lancia un agente estinguente verso la base. il fuoco, per estinguerlo.";
				piedepaginacards = "Ultime ispezioni|6 mesi fa";
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
				sessionModels.Idioma = lang;
				sessionModels.Idioma = "Si";
				sessionModels.Estado = 0;
				sessionModels.Modulos = modulos;
				sessionModels.Subtitulos = subtitulos;
				sessionModels.Descripciones = descripciones;
				sessionModels.piedepaginacards = piedepaginacards;
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
					Idioma= lang,
					Inicio="Si",
					Estado=0,
					Modulos=modulos,
					Subtitulos = subtitulos,
					Descripciones= descripciones,
					piedepaginacards=piedepaginacards,
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
			
			return Json(new { result = true, message = mess, url = Url.Action("LoginFacade", "Account") }, JsonRequestBehavior.AllowGet);
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
