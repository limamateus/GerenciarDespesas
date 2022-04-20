using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GerenciarDespesas.Context;
using GerenciarDespesas.Models;
using X.PagedList;
using GerenciarDespesas.ViewModels;

namespace GerenciarDespesas.Controllers
{
    public class DespesasController : Controller
    {
        private readonly Contexto _context;

        public DespesasController(Contexto context)
        {
            _context = context;
        }

        // GET: Despesas
        public async Task<IActionResult> Index(int? pagina)
        {
            const int itenspaginas = 10;
            int numeroPagina = (pagina ?? 1);

            ViewData["Meses"] = new SelectList(_context.Meses.Where(x => x.MesId == x.Salarios.MesId), "MesId", "Nome");
            var contexto = _context.Despesas.Include(d => d.Meses).Include(d => d.TipoDespesas).OrderBy(d=>d.MesId);
            return View(await contexto.ToPagedListAsync(numeroPagina,itenspaginas));
        }

        

        // GET: Despesas/Create
        public IActionResult Create()
        {
            ViewData["MesId"] = new SelectList(_context.Meses, "MesId", "Nome");
            ViewData["TipoDespesasId"] = new SelectList(_context.TipoDespesas, "TipoDespesasId", "Nome");
            return View();
        }

        // POST: Despesas/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DespesaId,MesId,TipoDespesasId,Valor")] Despesas despesas)
        {
            if (ModelState.IsValid)
            {  
                _context.Add(despesas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MesId"] = new SelectList(_context.Meses, "MesId", "Nome", despesas.MesId);
            ViewData["TipoDespesasId"] = new SelectList(_context.TipoDespesas, "TipoDespesasId", "Nome", despesas.TipoDespesasId);
            return View(despesas);
        }

        // GET: Despesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var despesas = await _context.Despesas.FindAsync(id);
            if (despesas == null)
            {
                return NotFound();
            }
            ViewData["MesId"] = new SelectList(_context.Meses, "MesId", "Nome", despesas.MesId);
            ViewData["TipoDespesasId"] = new SelectList(_context.TipoDespesas, "TipoDespesasId", "Nome", despesas.TipoDespesasId);
            return View(despesas);
        }

        // POST: Despesas/Edit
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DespesaId,MesId,TipoDespesasId,Valor")] Despesas despesas)
        {
            if (id != despesas.DespesaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(despesas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DespesasExists(despesas.DespesaId))
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
            ViewData["MesId"] = new SelectList(_context.Meses, "MesId", "Nome", despesas.MesId);
            ViewData["TipoDespesasId"] = new SelectList(_context.TipoDespesas, "TipoDespesasId", "Nome", despesas.TipoDespesasId);
            return View(despesas);
        }

      

        // POST: Despesas/Delete/5
        [HttpPost]
       
        public async Task<IActionResult> Delete(int id)
        {
            var despesas = await _context.Despesas.FindAsync(id);
            _context.Despesas.Remove(despesas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public JsonResult GastosTotaisMes(int mesId) // Retorno usando um ViewModels, ou seja, eu criu uma view models para juntar dados de duas ou mais models
        {
            GastosTotaisMesViewModel gastos = new GastosTotaisMesViewModel();

            gastos.ValorTotalGasto = _context.Despesas.Where(d => d.Meses.MesId == mesId).Sum(d => d.Valor); // Somar o valor das despesas
            gastos.Salario = _context.Salarios.Where(s => s.Meses.MesId == mesId).Select(s => s.Valor).FirstOrDefault();

            return Json(gastos);
        }

        public JsonResult GastosMes(int mesId) // Retorno usando uma quary
        {
            var quary = from despesas in _context.Despesas
                        where despesas.Meses.MesId == mesId
                        group despesas by despesas.TipoDespesas.Nome into g
                        select new
                        {
                            TipoDespesas = g.Key,
                            Valores = g.Sum(d => d.Valor)
                        };

            return Json(quary);
            
        }

        public JsonResult GastoTotais()
        {
            var query = _context.Despesas.Include(m => m.Meses).ToList().OrderBy(m => m.Meses.MesId)
                                                                        .GroupBy(m => m.Meses.MesId)
                                                                        .Select(d => new { NomeMeses = d.Select(x => x.Meses.Nome).Distinct(), Valores = d.Sum(x => x.Valor) });
            

            return Json(query);
        }

        private bool DespesasExists(int id)
        {
            return _context.Despesas.Any(e => e.DespesaId == id);
        }
    }

   
}
