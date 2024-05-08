using System;
using System.Collections.Generic;

namespace Mercy_Dev.Models;

public partial class Servicio
{
    public int IdServicios { get; set; }

    public string Mantenimiento { get; set; } = null!;

    public string InstalacionSO { get; set; } = null!;

    public string Limpieza { get; set; } = null!;

    public string InstalacionDrivers { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public int Valor { get; set; }

    public virtual ICollection<ClienteHasServicio> ClienteHasServicios { get; } = new List<ClienteHasServicio>();
}
