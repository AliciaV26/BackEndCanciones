namespace WebApiCanciones.Entidades
{
    public class Album
    {
        
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Artista { get; set; }

            public int CancionId { get; set; }

            public Cancion Cancion { get; set; }
        
    }
}
