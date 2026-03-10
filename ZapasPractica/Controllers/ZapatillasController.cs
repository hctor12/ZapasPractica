using Microsoft.AspNetCore.Mvc;
using ZapasPractica.Models;
using ZapasPractica.Repositories;

namespace ZapasPractica.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapatillas repo;

        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Zapatilla> zapas = await this.repo.GetZapatillasAsync();
            return View(zapas);
        }

        public async Task<IActionResult> Details(int id, int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            (int numRegistros, ImagenZapatilla imagen) = await this.repo.GetImagenZapatillaAsync(posicion.Value, id);
            int siguiente = posicion.Value + 1;
            if (siguiente > numRegistros)
            {
                siguiente = numRegistros;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
            }
            ViewData["ULTIMO"] = numRegistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["POSICION"] = posicion;
            ViewData["IMAGEN"] = imagen;
            Zapatilla zapa = await this.repo.FindZapatillaAsync(id);
            return View(zapa);
        }

        public async Task<IActionResult> GetImagenes(int posicion, int zapa)
        {
            if (zapa == null)
            {
                return NotFound();
            }

            (int numRegistros, ImagenZapatilla imagen) = await this.repo.GetImagenZapatillaAsync(posicion, zapa);
            int siguiente = posicion + 1;
            if (siguiente > numRegistros)
            {
                siguiente = numRegistros;
            }
            int anterior = posicion - 1;
            if (anterior < 1)
            {
                anterior = 1;
            }
            ViewData["ULTIMO"] = numRegistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["POSICION"] = posicion;
            return PartialView("_ImagenPartial", imagen);
        }

    }
}
