using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GerenciarDespesas.Context;
using GerenciarDespesas.Models;

namespace GerenciarDespesas.Controllers
{
    public class TipoDespesasController : Controller
    {
        private readonly Contexto _context;

        public TipoDespesasController(Contexto context)
        {
            _context = context;
        }

        // GET: TipoDespesas
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoDespesas.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string txtProcurar) // vou receber a informação do input
        {
            if (!String.IsNullOrEmpty(txtProcurar)) // se tiver alguma informação vou relalziar a consulta e trazer um lista
            {
                return View(await _context.TipoDespesas.Where(td => td.Nome.ToUpper().Contains(txtProcurar.ToUpper())).ToListAsync());

            }

            return View(await _context.TipoDespesas.ToListAsync()); // caso contrario trago todos 
        }




        // GET: TipoDespesas/Create
        public IActionResult Create()
        {
            return View();
        }


        public async Task<JsonResult> TipoDespesasExiste(string Nome)
        {
            if (await _context.TipoDespesas.AnyAsync(td => td.Nome.ToUpper() == Nome.ToUpper()))

                return Json("Esse tipo de despesa já existe !");




            return Json(true);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoDespesasId,Nome")] TipoDespesas tipoDespesas)
        {
            if (ModelState.IsValid)
            {
                TempData["Confirmacao"] = tipoDespesas.Nome + "foi cadastrado com sucesso";
                _context.Add(tipoDespesas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDespesas);
        }
        [HttpPost]
        public JsonResult AdicionarTipoDespesas(string txtDespesa) // Função para botão de criar um tipo de despesas quando for estiver criando um despesas e não tiver
        {
            if (!String.IsNullOrEmpty(txtDespesa))  // verifico se esta nulo ou vazio
            {
                if(!_context.TipoDespesas.Any(td=>td.Nome.ToUpper() == txtDespesa.ToUpper())) // verifico se já não está cadastrado
                {
                     TipoDespesas tipoDespesas = new TipoDespesas(); // crio uma novo tipo de despesas

                    tipoDespesas.Nome = txtDespesa; // atribuo a um variavale temporaria

                    _context.Add(tipoDespesas); // adiciono no banco 

                    _context.SaveChanges(); // Salvo 

                    return Json(true); // retorno para view com Verdadeiro 
                }
              

            }

            return Json(false);
        }

        // GET: TipoDespesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDespesas = await _context.TipoDespesas.FindAsync(id);
            if (tipoDespesas == null)
            {
                return NotFound();
            }
            return View(tipoDespesas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoDespesasId,Nome")] TipoDespesas tipoDespesas)
        {
            if (id != tipoDespesas.TipoDespesasId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Confirmacao"] = tipoDespesas.Nome + "foi atualizado com sucesso";
                    _context.Update(tipoDespesas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDespesasExists(tipoDespesas.TipoDespesasId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDespesas);
        }




        [HttpPost]

        public async Task<JsonResult> Delete(int id)
        {
            var tipoDespesas = await _context.TipoDespesas.FindAsync(id);
            TempData["Confirmacao"] = tipoDespesas.Nome + "foi excluido com sucesso";
            _context.TipoDespesas.Remove(tipoDespesas);
            await _context.SaveChangesAsync();
            return Json(tipoDespesas.Nome + " foi excluido com sucesso");
        }

        private bool TipoDespesasExists(int id)
        {
            return _context.TipoDespesas.Any(e => e.TipoDespesasId == id);
        }
    }
}
