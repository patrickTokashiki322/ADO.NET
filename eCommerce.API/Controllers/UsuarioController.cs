using eCommerce.API.Models;
using eCommerce.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private UsuarioRepository _repository;

        public UsuarioController()
        {
            _repository = new UsuarioRepository();
        }

        /*
         * CRUD
         * - GET - Obter lista de usuários
         * - GET - Obter o usuário passando o ID
         * - POST - Cadastrar um usuário
         * - PUT - Atualizar um usuário
         * - DELETE - Remover um usuário
         * **/

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.Get()); // http 200
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var usuario = _repository.Get(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Insert([FromBody]Usuario usuario)
        {
            _repository.Insert(usuario);
            return Ok(usuario);
        }

        [HttpPut]
        public IActionResult Update([FromBody]Usuario usuario)
        {
            _repository.Update(usuario);
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok();
        }

    }
}
