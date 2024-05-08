using System;
using System.Collections.Generic;

namespace Mercy_Dev.Models;

public partial class ClienteHasServicio
{
    public int ClienteIdCliente { get; set; }

    public int ServiciosIdServicios { get; set; }

    public DateTime? FechaHora { get; set; }

    public string? Estado { get; set; }

    public virtual Cliente ClienteIdClienteNavigation { get; set; } = null!;

    public virtual Servicio ServiciosIdServiciosNavigation { get; set; } = null!;
}
