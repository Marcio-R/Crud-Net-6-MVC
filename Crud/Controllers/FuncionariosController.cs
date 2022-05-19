using Crud.Models;
using Crud.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly FuncionarioService _funcionarioService;

        public FuncionariosController(FuncionarioService funcionario)
        {
            _funcionarioService = funcionario;
        }

        //Me traz uma lista de funcionarios
        public async Task<IActionResult> Index()
        {
            var list = await _funcionarioService.FindAllAsync();
            return View(list);
        }
        //Só me traz a tela de cadastro
        public async Task<IActionResult> Create()
        {
            return View();
        }

        //nameof ele permite mente o nome do metodo caso ele mude.
        //Esse create insere no banco atraves do verbo Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Funcionario funcionario)
        {
            await _funcionarioService.InsertAsync(funcionario);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = await _funcionarioService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _funcionarioService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = await _funcionarioService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = await _funcionarioService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return BadRequest();
            }
            await _funcionarioService.UpdateAsync(funcionario);
            return RedirectToAction(nameof(Index));
        }
    }
}
