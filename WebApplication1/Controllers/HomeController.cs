using LogicBo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filters;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Access]
    public class HomeController : Controller
    {
        #region Properties
        HomeBo _homeBo = new HomeBo();
        string _pathImge = ConfigurationManager.AppSettings["PathImage"].ToString();
        #endregion
        private int _index = 0;

        public int Index1 { get => _index; set => _index = value; }

        public ActionResult Index(string modulo)
        {
            SessionModels sessionModels = base.Session["SessionUser"] as SessionModels;
            sessionModels.ModuloSeleccionado = modulo;
            string Welcome = string.Empty;
            string invite = string.Empty;
            string textopp1 = string.Empty;
            string textopp2 = string.Empty;
            string textopp3 = string.Empty;
            string textopp4 = string.Empty;
            
            if (sessionModels.ModuloSeleccionado == "IC")
            {
                if (sessionModels.Idioma == "english")
                {
                    Welcome = "WELCOME TO THE PPROTECC 2.0 PORTAL - LIFTING OF LOADS";
                    invite = "WE INVITE YOU TO BE PART OF THIS EXPERIENCE!";
                    textopp1 = "PPROTECC, is a software designed to guarantee the real - time administration of the program for work at heights of the ENEL organization, in this you will find";
                    textopp2 = "different modules that will guide you in the management of the program, for each of the organization's headquarters.";
                    textopp3 = "PPROTECC was conceived as a management tool for the prevention of work accidents at heights, to strengthen the application of organizational policies";
                    textopp4 = "and ensure timely compliance with current legislation and regulations.";
                }
                else if (sessionModels.Idioma == "spain")
                {
                    Welcome = "BIENVENIDO AL PORTAL PPROTECC 2.0 - IZAGE DE CARGAS";
                    invite = "¡TE INVITAMOS A HACER PARTE DE ESTA EXPERIENCIA!";
                    textopp1 = "PPROTECC,  es un software diseñado para garantizar la administración en tiempo real del programa para trabajo en alturas de la organización ENEL,  en este encontraras";
                    textopp2 = "diferentes módulos que te guiaran en la gestión del programa, para cada una de las sedes de la organización";
                    textopp3 = "PPROTECC fue pensado como herramienta de gestión para la prevención de accidentes de trabajo en alturas, para fortalecer la aplicación de las políticas organizacionales  y";
                    textopp4 = "velar por el cumplimiento oportuno de la  legislación y normatividad vigente.";
                }
                else if (sessionModels.Idioma == "italiano")
                {
                    Welcome = "BENVENUTO NEL PORTALE PPROTECC 2.0 - SOLLEVAMENTO CARICHI";
                    invite = "VI INVITIAMO A FAR PARTE DI QUESTA ESPERIENZA";
                    textopp1 = "PPROTECC, è un software pensato per garantire l'amministrazione in tempo reale del programma per i lavori in quota dell'organizzazione ENEL, in esso troverete";
                    textopp2 = "diversi moduli che ti guideranno nella gestione del programma, per ciascuna sede dell'organizzazione.";
                    textopp3 = "PPROTECC nasce come strumento gestionale per la prevenzione degli infortuni sul lavoro in quota, per rafforzare l'applicazione";
                    textopp4 = "delle politiche organizzative e garantire il tempestivo rispetto della normativa e dei regolamenti vigenti.";
                }

                sessionModels.Welcome = Welcome;
                sessionModels.Invite = invite;
                sessionModels.TextoPro1 = textopp1;
                sessionModels.TextoPro2 = textopp2;
                sessionModels.TextoPro3 = textopp3;
                sessionModels.TextoPro4 = textopp4;

            }


          

            ViewBag.pathImage = _pathImge;
            var model = _homeBo.GetBannerImages(Server.MapPath(_pathImge));
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return PartialView();
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}