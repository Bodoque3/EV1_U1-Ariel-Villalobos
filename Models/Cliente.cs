using System;
using System.Collections.Generic;

namespace Mercy_Dev.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string NombreCli { get; set; } = null!;

    public string ApellidoCli { get; set; } = null!;

    public int TelefonoCli { get; set; }

    public string CorreoCli { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateTime FechaHora { get; set; }

    public virtual ICollection<ClienteHasServicio> ClienteHasServicios { get; } = new List<ClienteHasServicio>();
}
