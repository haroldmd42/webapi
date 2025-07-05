using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Services;

public class TareaService : ITareaService
{
    TareasContext context;

    public TareaService(TareasContext dbContext)
    {
        context = dbContext;
    }
    public IEnumerable<Tarea> Get()
    {
        return context.tareas;
    }
    public async Task Save(Tarea tarea)
    {
        context.Add(tarea);
        await context.SaveChangesAsync();
    }

    public async Task Update(Guid id, Tarea tarea)
    {
        var tareaActual = context.tareas.Find(id);
        if (tareaActual != null)
        {
            tareaActual.Titulo = tarea.Titulo;
            tareaActual.Descripcion = tarea.Descripcion;
            tareaActual.PrioridadTarea = tarea.PrioridadTarea;

            await context.SaveChangesAsync();
        }

    }
    public async Task Delete(Guid id)
    {
        var categoriaActual = context.categorias.Find(id);
        if (categoriaActual != null)
        {
            context.Remove(categoriaActual);

            await context.SaveChangesAsync();
        }

    }

   
}

public interface ITareaService
{
    IEnumerable<Tarea> Get();
    Task Save(Tarea tarea);
    Task Update(Guid id, Tarea tarea);
    Task Delete(Guid id);
}