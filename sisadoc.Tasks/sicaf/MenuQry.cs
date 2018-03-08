using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sisadoc.Domain.Reposositories;
using sisadoc.Domain.sicaf;
namespace sisadoc.Tasks.sicaf
{
   public class MenuQry
    {

        private readonly IMenuRepository op_apl_tpousr;
        private readonly IOpcionAplicacionRepository OpcionesMenu;
        public MenuQry(IMenuRepository op_apl_tpousr, IOpcionAplicacionRepository opcionesMenu)
        {
            this.op_apl_tpousr = op_apl_tpousr;
            this.OpcionesMenu = opcionesMenu;
        }
        /// <summary>
        /// Devuelve todos los tipos de identificación disponibles
        /// </summary>
        /// <returns>Lista delos tipos de identificación</returns>
    public IList<OpcionesUsuario> GetOpcTpoUsr(int tpo_usr)
        {
            return op_apl_tpousr.GetOpcTpoUsr(tpo_usr);
        }
    public IList<OpcionAplicacion> GetUrlMenu(string Idurl)
    {
        return OpcionesMenu.GetUrlMenu(Idurl);
    }
      
    }
}
