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
    public class SalariosController : Controller
    {
        private readonly Contexto _context;

        public SalariosController(Contexto context)
        {
            _context = context;
        }

        // GET: Salarios
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Salarios.Include(s => s.Meses).OrderBy(S => S.MesId);
            return View(await contexto.ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Index(string txtProcurar) // vou receber a informação do input
        {
            if (!String.IsNullOrEmpty(txtProcurar)) // se tiver alguma informação vou relalziar a consulta e trazer um lista
            {
                return View(await _context.Salarios.Include(s => s.Meses).Where(m => m.Meses.Nome.ToUpper().Contains(txtProcurar.ToUpper())).ToListAsync());

            }

            return View(await _context.Salarios.Include(s => s.Meses).OrderBy(S => S.MesId).ToListAsync()); // caso contrario trago todos 
        }




        // GET: Salarios/Create
        public IActionResult Create()
        {
            ViewData["MesId"] = new SelectList(_context.Meses, "MesId", "Nome");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalarioId,MesId,Valor")] Salario salario)
        {
            if (ModelState.IsValid)
            {
                TempData["Confirmacao"] = "foi cadastrado com sucesso";
                _context.Add(salario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MesId"] = new SelectList(_context.Meses.Where(s => s.MesId != salario.MesId), "MesId", "Nome", salario.MesId);
            return View(salario);
        }

        // GET: Salarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salario = await _context.Salarios.FindAsync(id);
            if (salario == null)
            {
                return NotFound();
            }
            ViewData["MesId"] = new SelectList(_context.Meses.Where(s => s.MesId == salario.MesId), "MesId", "Nome", salario.MesId);
            return View(salario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalarioId,MesId,Valor")] Salario salario)
        {
            if (id != salario.SalarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salario);
                    await _context.SaveChangesAsync();
                    TempData["Confirmacao"] = "foi atualizado com sucesso";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalarioExists(salario.SalarioId))
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
            ViewData["MesId"] = new SelectList(_context.Meses.Where(s => s.MesId != salario.MesId), "MesId", "Nome", salario.MesId);
            return View(salario);
        }

   


        [HttpPost]     
        public async Task<IActionResult> Delete(int id)
        {
            var salario = await _context.Salarios.FindAsync(id);
            _context.Salarios.Remove(salario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalarioExists(int id)
        {
            return _context.Salarios.Any(e => e.SalarioId == id);
        }
    }
}
