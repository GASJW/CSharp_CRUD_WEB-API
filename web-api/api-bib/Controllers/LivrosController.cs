using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api_bib.Controllers
{
    public class LivrosController : ApiController
    {
        // GET: api/Livros
        public IHttpActionResult Get()
        {
            try
            {
                Repositories.Database.Entity.Livro.ConnectionString = api_bib.Configuration.Database.SQLServer.Parameters.getConnectionString();
                List<Models.Livro> livros =  Repositories.Database.Entity.Livro.get();
                return Ok(livros);
            }
            catch (Exception ex)
            {
                Utils.Logger.PathLog = api_bib.Configuration.Logger.Parameters.getPathLog();
                Utils.Logger.writer(ex);
                return InternalServerError();
            }
            
        }

        // GET: api/Livros/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                Repositories.Database.Entity.Livro.ConnectionString = api_bib.Configuration.Database.SQLServer.Parameters.getConnectionString();
                Models.Livro livro = Repositories.Database.Entity.Livro.get(id);
                if (livro != null)
                    return Ok(livro);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Utils.Logger.PathLog = api_bib.Configuration.Logger.Parameters.getPathLog();
                Utils.Logger.writer(ex);
                return InternalServerError();
            }
        }

        // POST: api/Livros
        public IHttpActionResult Post(Models.Livro livro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Repositories.Database.Entity.Livro.ConnectionString = api_bib.Configuration.Database.SQLServer.Parameters.getConnectionString();
                    Repositories.Database.Entity.Livro.add(livro);
                    return Ok(livro);
                }
                catch (Exception ex)
                {
                    Utils.Logger.PathLog = api_bib.Configuration.Logger.Parameters.getPathLog();
                    Utils.Logger.writer(ex);
                    return InternalServerError();
                }
            }
            else
            {
                //TODO: Montar uma lógica para construir mensagem de erro. (não fazer isso no controlador, fazer isso em outro lugar e invocar aqui no controlador).
                //Utils.Logger.writer();
                return BadRequest();
            }
        }

        // PUT: api/Livros/5
        public IHttpActionResult Put(int id, Models.Livro livro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Repositories.Database.Entity.Livro.get(id) == null)
                        return NotFound();

                    Repositories.Database.Entity.Livro.update(livro);
                    return Ok();
                }
                catch (Exception ex)
                {
                    Utils.Logger.PathLog = api_bib.Configuration.Logger.Parameters.getPathLog();
                    Utils.Logger.writer(ex);
                    return InternalServerError();
                }
            }
            else
            {
                //TODO: Montar uma lógica para construir mensagem de erro. (não fazer isso no controlador, fazer isso em outro lugar e invocar aqui no controlador).
                //Utils.Logger.writer();
                return BadRequest();
            }
        }

        // DELETE: api/Livros/5
        public IHttpActionResult Delete(int id)
        {
            try
            {

                Repositories.Database.Entity.Livro.ConnectionString = api_bib.Configuration.Database.SQLServer.Parameters.getConnectionString();
                
                if (Repositories.Database.Entity.Livro.get(id) == null)
                    return NotFound();

                Repositories.Database.Entity.Livro.delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Utils.Logger.PathLog = api_bib.Configuration.Logger.Parameters.getPathLog();
                Utils.Logger.writer(ex);
                return InternalServerError();
            }
        }
    }
}
