using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ZapasPractica.Data;
using ZapasPractica.Models;

#region PROCEDURE

/*create procedure SP_IMAGEN_ZAPATILLA
(@posicion int, @idzapa int, @registros int OUT)
as
	select @registros = count(*) from IMAGENESZAPASPRACTICA
	where IDPRODUCTO = @idzapa
	select IDIMAGEN, IDPRODUCTO, IMAGEN from
	(select cast(row_number() over (order by IDIMAGEN) as int)
	POSICION, IDIMAGEN, IDPRODUCTO, IMAGEN
	from IMAGENESZAPASPRACTICA
	where IDPRODUCTO = @idzapa) QUERY
	where (QUERY.POSICION = @posicion)
go*/

#endregion

namespace ZapasPractica.Repositories
{
    public class RepositoryZapatillas
    {
        private ZapatillasContext context;

        public RepositoryZapatillas(ZapatillasContext context)
        {
            this.context = context;
        }

        public async Task<List<Zapatilla>> GetZapatillasAsync()
        {
            return await this.context.Zapatillas.ToListAsync();
        }

        public async Task<Zapatilla> FindZapatillaAsync(int id)
        {
            return await this.context.Zapatillas.FindAsync(id);
        }

        public async Task<(int, ImagenZapatilla)> GetImagenZapatillaAsync(int posicion, int id)
        {
            string sql = "SP_IMAGEN_ZAPATILLA @posicion, @idzapa, @registros out";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamIdzapa = new SqlParameter("@idzapa", id);
            SqlParameter pamRegistros = new SqlParameter("@registros", 0);
            pamRegistros.Direction = ParameterDirection.Output;
            ImagenZapatilla imagen = await this.context.Imagenes.FromSqlRaw(sql, pamPosicion, pamIdzapa, pamRegistros).AsAsyncEnumerable().FirstOrDefaultAsync();
            int registros = (pamRegistros.Value == DBNull.Value) ? 0 : Convert.ToInt32(pamRegistros.Value);
            return (registros, imagen);
        }
    }
}
