using Api.Epsas.Controllers.Common;
using Api.Epsas.Entities.common;
using Api.Epsas.Entities.Dtos;
using Api.Epsas.Interfaces;

namespace Api.Epsas.Controllers;

public class UsersController : BaseApiController
{

    private IUsuariosServices _usuarios;
    public UsersController(IUsuariosServices usuarios)
    {
        _usuarios = usuarios;
    }

    [HttpGet]
    public ResponseQuery<SegUsuarios> GetAll()
    {
        var list = new ResponseQuery<SegUsuarios>();
        list.Data = _usuarios.GetAll();
        list.State = list.Data.Count > 0 ? State.Success : State.NoData;
        list.Message = list.Data.Count > 0 ? $"Se Obtuvieron {list.Data.Count} registros" : "consulta vacia";
        return list;
    }


    [HttpGet("Estado")]
    public async Task<ResponseQuery<ParamClasificador>> GetEstado()
    {
        var list = new ResponseQuery<ParamClasificador>();
        list.Data = await _usuarios.GetEstados();
        list.State = list.Data.Count > 0 ? State.Success : State.NoData;
        list.Message = list.Data.Count > 0 ? $"Se Obtuvieron {list.Data.Count} registros" : "consulta vacia";
        return list;
    }

    [HttpGet("Roles")]
    public ResponseQuery<SegRoles> GetAllRoles()
    {
        var list = new ResponseQuery<SegRoles>();
        list.Data = _usuarios.GetAllRoles();
        list.State = list.Data.Count > 0 ? State.Success : State.NoData;
        list.Message = list.Data.Count > 0 ? $"Se Obtuvieron {list.Data.Count} registros" : "consulta vacia";
        return list;
    }

    [HttpGet("{id}")]
    public ResponseEntity GetId(int id)
    {
        var vent = new ResponseEntity();
        vent.Data = _usuarios.GetById(id);
        vent.State = vent.Data != null ? State.Success : State.NoData;
        vent.Message = vent.Data != null ? $"Se Obtuvo el registro {id}" : "consulta vacia";
        return vent;
    }

    [HttpPost]
    public async Task<ResponseOperation<UsuariosDto>> SaveChage(UsuariosDto req)
    {
        var resp = new ResponseOperation<UsuariosDto>();
        var entidad = await _usuarios.Create(req);

        resp.Data = req;
        resp.State = req != null ? State.Success : State.Warning;
        resp.Message = req != null ? Resources.Genericos.SaveChangeSucess : Resources.Genericos.ErrorSave;

        return resp;
    }

    [HttpPut("{id}")]
    public async Task<ResponseOperation<UsuariosDto>> UpdateChange(int id, UsuariosDto req)
    {
        var resp = new ResponseOperation<UsuariosDto>();

        if (id != req.IdsegUsuarios)
        {
            resp.Message = "El id no corresponde al id de registro";
            resp.State = State.Warning;
            return resp;
        }

        var entidad = await _usuarios.Update(req);

        resp.Data = req;
        resp.State = req != null ? State.Success : State.Warning;
        resp.Message = req != null ? Resources.Genericos.SaveChangeSucess : Resources.Genericos.ErrorSave;

        return resp;

    }


    [HttpDelete("{id}")]
    public async Task<ResponseOperation<SegUsuarios>> Delete(int id)
    {
        var resp = new ResponseOperation<SegUsuarios>();
        var entidad = await _usuarios.Delete(id);
        resp.State = entidad > 0 ? State.Success : State.Warning;
        resp.Message = entidad > 0 ? Resources.Genericos.SaveChangeSucess : Resources.Genericos.ErrorSave;
        return resp;
    }

}