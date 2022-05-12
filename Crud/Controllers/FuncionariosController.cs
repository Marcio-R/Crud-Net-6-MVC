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
        public IActionResult Index()
        {
            var list = _funcionarioService.FindAll();
            return View(list);
        }
        //Só me traz a tela de cadastro
        public IActionResult Create()
        {
            return View();
        }

        //nameof ele permite mente o nome do metodo caso ele mude.
        //Esse create insere no banco atraves do verbo Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Funcionario funcionario)
        {
            _funcionarioService.Insert(funcionario);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _funcionarioService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _funcionarioService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _funcionarioService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var obj = _funcionarioService.FindById(id.Value);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Funcionario funcionario)
        {
            if(id != funcionario.Id)
            {
                return BadRequest();
            }
            _funcionarioService.Update(funcionario);
            return RedirectToAction(nameof(Index));
        }
    }
}
