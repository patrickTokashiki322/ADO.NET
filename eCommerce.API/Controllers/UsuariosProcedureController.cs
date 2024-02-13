﻿using eCommerce.API.Models;
using eCommerce.API.Repositories;
using eCommerce.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosProcedureController : ControllerBase
    {
        private IUsuarioRepository _repository;

        public UsuariosProcedureController()
        {
            _repository = new UsuarioProcedureRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.Get()); // http 200
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            try
            {
                var usuario = _repository.Get(id);

                if (usuario == null)
                {
                    return NotFound();
                }

                return Ok(usuario);
            }
            catch (Exception)
            {

            }

            throw new Exception("Erro ao buscar usuário por ID!");
        }

        [HttpPost]
        public IActionResult Insert([FromBody] Usuario usuario)
        {

            try
            {
                _repository.Insert(usuario);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] Usuario usuario)
        {
            try
            {
                _repository.Update(usuario);
                return Ok(usuario);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok();
        }

    }
}
