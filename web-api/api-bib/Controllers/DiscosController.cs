using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api_bib.Controllers
{
    public class DiscosController : ApiController
    {
        // GET: api/Discos
        public IHttpActionResult Get()
        {
            try
            {
                Repositories.Database.Entity.Disco.ConnectionString = api_bib.Configuration.Database.SQLServer.Parameters.getConnectionString();
                List<Models.Disco> discos = Repositories.Database.Entity.Disco.get();
                return Ok(discos);
            }
            catch (Exception ex)
            {
                Utils.Logger.PathLog = api_bib.Configuration.Logger.Parameters.getPathLog();
                Utils.Logger.writer(ex);
                return InternalServerError();
            }

        }

        // GET: api/Discos/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                Repositories.Database.Entity.Disco.ConnectionString = api_bib.Configuration.Database.SQLServer.Parameters.getConnectionString();
                Models.Disco disco = Repositories.Database.Entity.Disco.get(id);
                if (disco != null)
                    return Ok(disco);
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

        // POST: api/Discos
        public IHttpActionResult Post(Models.Disco disco)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Repositories.Database.Entity.Disco.ConnectionString = api_bib.Configuration.Database.SQLServer.Parameters.getConnectionString();
                    Repositories.Database.Entity.Disco.add(disco);
                    return Ok(disco);
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

        // PUT: api/Discos/5
        public IHttpActionResult Put(int id, Models.Disco disco)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Repositories.Database.Entity.Disco.get(id) == null)
                        return NotFound();

                    Repositories.Database.Entity.Disco.update(disco);
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

        // DELETE: api/Discos/5
        public IHttpActionResult Delete(int id)
        {
            try
            {

                Repositories.Database.Entity.Disco.ConnectionString = api_bib.Configuration.Database.SQLServer.Parameters.getConnectionString();

                if (Repositories.Database.Entity.Disco.get(id) == null)
                    return NotFound();

                Repositories.Database.Entity.Disco.delete(id);
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
