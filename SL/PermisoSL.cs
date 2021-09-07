using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using SL.PatronComposite;

namespace SL
{
    public class PermisoSL
    {
        private PermisoMapper PermisoMapper = new PermisoMapper();
        private AutorizacionSL gestorAutorizacion = new AutorizacionSL();

        public List<PermisoBE> ListarPermisos()
        {
            PermisoMapper m = new PermisoMapper();
            return m.ListarPermisos();
        }

        public int InsertarPermiso(PermisoBE permiso, UsuarioBE usuario)
        {
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Crear Permisos"), usuario))
            {
                PermisoMapper m = new PermisoMapper();
                return m.InsertarPermiso(permiso);
            }
            else return -2;
        }

        public List<PatronComposite.ComponentPermiso> ListarPermisosArbol(UsuarioBE usuario)
        {
            List<PatronComposite.ComponentPermiso> gruposDePermisos = new List<PatronComposite.ComponentPermiso>();
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Gestionar Permisos"), usuario))
            {
                PermisoBE permisoRoot = new PermisoBE();
                permisoRoot.CodPermiso = 0;
                List<PermisoBE> PermisosGrupo = PermisoMapper.ListarPermisosPorPadre(permisoRoot);

                foreach (PermisoBE permiso in PermisosGrupo)
                {
                    PatronComposite.CompositePermiso FamiliaPermiso = new PatronComposite.CompositePermiso(permiso);
                    gruposDePermisos.Add(FamiliaPermiso);
                    List<PermisoBE> PermisosHijos = PermisoMapper.ListarPermisosPorPadre(permiso);
                    SubNodoAgregar(FamiliaPermiso, PermisosHijos);
                }

            }
            else gruposDePermisos = null;

            return gruposDePermisos;
        }

        /// <summary>
        /// Función recursiva para agregar cada una de las ramas de permisos
        /// </summary>
        /// <param name="nodo"></param>
        /// <param name="hijos"></param>
        private void SubNodoAgregar(PatronComposite.ComponentPermiso nodo, List<PermisoBE> hijos)
        {
            if (hijos.Count > 0)
            {
                foreach (PermisoBE hijo in hijos)
                {
                    List<PermisoBE> newHijos = new List<PermisoBE>();
                    newHijos = PermisoMapper.ListarPermisosPorPadre(hijo);

                    if (newHijos.Count() == 0)
                    {
                        nodo.Agregar(new PatronComposite.HojaPermiso(hijo));
                    }
                    else
                    {
                        PatronComposite.CompositePermiso compositePermiso = new PatronComposite.CompositePermiso(hijo);
                        nodo.Agregar(compositePermiso);
                        SubNodoAgregar(compositePermiso, newHijos);
                    }
                }
            }
        }

        public List<ComponentPermiso> ListarPermisosPorTipoUsuario(TipoUsuarioBE tipoUsuario, UsuarioBE usuario)
        {
            List<PatronComposite.ComponentPermiso> gruposDePermisos = new List<PatronComposite.ComponentPermiso>();
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Gestionar Permisos"), usuario))
            {
                List<PermisoBE> PermisosGrupo = PermisoMapper.ListarPermisosPorTipoUsuario(tipoUsuario);

                foreach (PermisoBE permiso in PermisosGrupo)
                {
                    if (permiso.CodPermisoPadre == null)
                    {
                        PatronComposite.CompositePermiso FamiliaPermiso = new PatronComposite.CompositePermiso(permiso);
                        gruposDePermisos.Add(FamiliaPermiso);
                        List<PermisoBE> PermisosHijos = PermisoMapper.ListarPermisosPorPadre(permiso);
                        SubNodoAgregar(FamiliaPermiso, PermisosHijos);
                    }
                }
            }
            else gruposDePermisos = null;
            return gruposDePermisos;
        }

        public int RelacionarPermisos(PermisoBE permisoPadre, PermisoBE permisoHijo, UsuarioBE usuario)
        {
            int retVal = -1;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Gestionar Permisos"), usuario))
            {
                PermisoMapper m = new PermisoMapper();
                retVal = m.RelacionarPermisos(permisoPadre, permisoHijo);
                //Si tuvo permisos para realizar algún tipo de cambio, por las dudas,
                //debo actualizar la lista de los permisos del usuarioAutenticado, para que se actualicen
                //qué acciones puede realizar y qué no
                gestorAutorizacion.CargarPermisosAlUsuario(ref usuario);
            }
            return retVal;
        }

        public int QuitarRelacionPermisos(PermisoBE permisoPadre, PermisoBE permisoHijo, UsuarioBE usuario)
        {
            int retVal = -1;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Gestionar Permisos"), usuario))
            {
                PermisoMapper m = new PermisoMapper();
                retVal = m.QuitarRelacionPermisos(permisoPadre, permisoHijo);
                //Si tuvo permisos para realizar algún tipo de cambio, por las dudas,
                //debo actualizar la lista de los permisos del usuarioAutenticado, para que se actualicen
                //qué acciones puede realizar y qué no
                gestorAutorizacion.CargarPermisosAlUsuario(ref usuario);
            }
            return retVal;
        }

        public int InsertarPermisoPorTipoUsuario(TipoUsuarioBE tipoUsuario, PermisoBE permiso, UsuarioBE usuario)
        {
            int retVal = -1;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Asignar Permisos"), usuario))
            {
                PermisoMapper m = new PermisoMapper();
                retVal = m.InsertarPermisoPorTipoUsuario(tipoUsuario, permiso);
                //Si tuvo permisos para realizar algún tipo de cambio, por las dudas,
                //debo actualizar la lista de los permisos del usuarioAutenticado, para que se actualicen
                //qué acciones puede realizar y qué no
                gestorAutorizacion.CargarPermisosAlUsuario(ref usuario);
            }
            return retVal;
        }

        public int EliminarPermiso(PermisoBE permiso, UsuarioBE usuarioAutenticado)
        {
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Eliminar Permisos"), usuarioAutenticado))
            {
                PermisoMapper m = new PermisoMapper();
                return m.EliminarPermiso(permiso);
            }
            else return -1;
        }

        public int EliminarPermisoPorTipoUsuario(TipoUsuarioBE tipoUsuario, PermisoBE permiso, UsuarioBE usuario)
        {
            int retVal = -1;
            if (gestorAutorizacion.ValidarPermisoUsuario(new PermisoBE("Quitar permisos"), usuario))
            {
                PermisoMapper m = new PermisoMapper();
                retVal = m.EliminarPermisoPorTipoUsuario(tipoUsuario, permiso);
                //Si tuvo permisos para realizar algún tipo de cambio, por las dudas,
                //debo actualizar la lista de los permisos del usuarioAutenticado, para que se actualicen
                //qué acciones puede realizar y qué no
                gestorAutorizacion.CargarPermisosAlUsuario(ref usuario);
            }
            return retVal;
        }
    }
}
