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
        public  async Task<List<Funcionario>> FindAllAsync()
        {
            return await _context.Funcionario.ToListAsync();

        }
        public async Task InsertAsync(Funcionario obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Funcionario> FindByIdAsync(int id)
        {
            return await _context.Funcionario.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Funcionario.FindAsync(id);
            _context.Funcionario.Remove(obj);
            await _context.SaveChangesAsync();
        }

        //Execeções são capituradas pelo serviço e relançadas na formar de execeções para o serviço
        //para controller
        public async Task UpdateAsync(Funcionario obj)
        {
            bool hasAny = await _context.Funcionario.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new Exception();
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }
    }
}
