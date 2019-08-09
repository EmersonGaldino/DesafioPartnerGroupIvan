using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ivan.Models;



namespace DesafioPartnerGroup.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Marca marca = new Marca() { Nome = "Mac Donalds" };
            Marca.Insert(marca);
            marca.Nome = $"Mac Donalds {DateTime.Now}";
            Marca.Update(marca);

            marca = Marca.SelectSingle(1);
            IList<Marca> marcas = Marca.Select();



            Patrimonio patrimonio = new Patrimonio() { Nome = "Cadeira", MarcaID = marca.MarcaID };
            Patrimonio.Insert(patrimonio);
            patrimonio.Nome = $"Mesa {DateTime.Now}";
            Patrimonio.Update(patrimonio);

            patrimonio = Patrimonio.SelectSingle(1);
            IList<Patrimonio> patrimonios = Patrimonio.Select();

            ViewBag.Title = $"Nome = {patrimonio.Nome}, MarcaID = {patrimonio.MarcaID}, Descricao = {patrimonio.Descricao}, NumeroDoTombo = {patrimonio.NumeroDoTombo}";


            return View();
        }
    }
}
