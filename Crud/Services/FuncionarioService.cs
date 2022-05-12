using Crud.Data;
using Crud.Models;
using Microsoft.EntityFrameworkCore;

namespace Crud.Services
{
    public class FuncionarioService
    {
        private readonly CrudContext _context;

        public FuncionarioService(CrudContext context)
        {
            _context = context;
        }
        public List<Funcionario> FindAll()
        {
            return _context.Funcionario.ToList();

        }
        public void Insert(Funcionario obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
        public Funcionario FindById(int id)
        {
            return _context.Funcionario.FirstOrDefault(x => x.Id == id);
        }
        public void Remove(int id)
        {
            var obj = _context.Funcionario.Find(id);
            _context.Funcionario.Remove(obj);
            _context.SaveChanges();
        }

        //Execeções são capituradas pelo serviço e relançadas na formar de execeções para o serviço
        //para controller
        public void Update(Funcionario obj)
        {
            if (!_context.Funcionario.Any(x => x.Id == obj.Id))
            {
                throw new Exception();
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }
    }
}
