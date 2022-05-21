using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api_bib.Controllers
{
    public class FuncionariosController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                Repositories.Database.Entity.Funcionario.ConnectionString = api_bib.Configuration.Database.SQLServer.Parameters.getConnectionString();
                List<Models.Funcionario> funcionarios = Repositories.Database.Entity.Funcionario.get();
                return Ok(funcionarios);
            }
            catch (Exception ex)
            {
                Utils.Logger.PathLog = api_bib.Configuration.Logger.Parameters.getPathLog();
                Utils.Logger.writer(ex);
                return InternalServerError();
            }

        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                Repositories.Database.Entity.Funcionario.ConnectionString = api_bib.Configuration.Database.SQLServer.Parameters.getConnectionString();
                Models.Funcionario funcionario = Repositories.Database.Entity.Funcionario.get(id);
                if (funcionario != null)
                    return Ok(funcionario);
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

        public IHttpActionResult Post(Models.Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Repositories.Database.Entity.Funcionario.ConnectionString = api_bib.Configuration.Database.SQLServer.Parameters.getConnectionString();
                    Repositories.Database.Entity.Funcionario.add(funcionario);
                    return Ok(funcionario);
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
                return BadRequest();
            }
        }

        public IHttpActionResult Put(int id, Models.Funcionario funcionario)
        {
            if (id == funcionario.Id && ModelState.IsValid)
            {
                try
                {
                    Repositories.Database.Entity.Funcionario.ConnectionString = api_bib.Configuration.Database.SQLServer.Parameters.getConnectionString();

                    if (Repositories.Database.Entity.Funcionario.get(id) == null)
                        return NotFound();

                    Repositories.Database.Entity.Funcionario.update(funcionario);
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
                return BadRequest();
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                Repositories.Database.Entity.Funcionario.ConnectionString = api_bib.Configuration.Database.SQLServer.Parameters.getConnectionString();

                if (Repositories.Database.Entity.Funcionario.get(id) == null)
                    return NotFound();

                Repositories.Database.Entity.Funcionario.delete(id);
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