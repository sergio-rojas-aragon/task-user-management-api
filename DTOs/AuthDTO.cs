namespace GTU.Api.DTOs
{
    public class AuthDTO
    {

        //user.Id, user.Nombre, user.Email, token
        public bool Estado { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public string Mensaje { get; set; }


    }
}
