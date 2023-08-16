using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dominio.Context;
using Dominio.Models;
using Microsoft.AspNetCore.Authorization;

namespace ECJ_Compras.Controllers
{
    public class TransacoesController : BaseController
    {
        private readonly EjcContext _context;

        public TransacoesController(EjcContext context)
        {
            _context = context;
        }

        // GET: Transacaos
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var ejcContext = _context.Transacoes.Include(t => t.Usuario);
            return View(await ejcContext.ToListAsync());
        }

        // GET: Transacaos/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transacoes == null)
            {
                return NotFound();
            }

            var transacao = await _context.Transacoes
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transacao == null)
            {
                return NotFound();
            }

            return View(transacao);
        }

        // GET: Transacaos/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Usuarios, "Id", "Login");
            return View();
        }

        // POST: Transacaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,Tipo,Valor,Data,MetodoPagamento,IdUsuario")] Transacao transacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Usuarios, "Id", "Login", transacao.Id);
            return View(transacao);
        }

        // GET: Transacaos/Edit/5
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transacoes == null)
            {
                return NotFound();
            }

            var transacao = await _context.Transacoes.FindAsync(id);
            if (transacao == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Usuarios, "Id", "Login", transacao.Id);
            return View(transacao);
        }

        // POST: Transacaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,Tipo,Valor,Data,MetodoPagamento,IdUsuario")] Transacao transacao)
        {
            if (id != transacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransacaoExists(transacao.Id))
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
            ViewData["Id"] = new SelectList(_context.Usuarios, "Id", "Login", transacao.Id);
            return View(transacao);
        }

        // GET: Transacaos/Delete/5
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transacoes == null)
            {
                return NotFound();
            }

            var transacao = await _context.Transacoes
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transacao == null)
            {
                return NotFound();
            }

            return View(transacao);
        }

        // POST: Transacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "adm")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transacoes == null)
            {
                return Problem("Entity set 'EjcContext.Transacoes'  is null.");
            }
            var transacao = await _context.Transacoes.FindAsync(id);
            if (transacao != null)
            {
                _context.Transacoes.Remove(transacao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransacaoExists(int id)
        {
          return (_context.Transacoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
