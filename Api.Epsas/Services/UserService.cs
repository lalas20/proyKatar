
using BCrypt.Net;
using Api.Epsas.DataContext;
using Api.Epsas.Entities;
using Api.Epsas.Entities.common;
using Api.Epsas.Interfaces;
using Api.Epsas.Interfaces.common;
using Microsoft.EntityFrameworkCore;
using Api.Epsas.Entities.Dtos;

namespace Api.Epsas.Services;
public class UserService : IUsuariosServices
{
    private AplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogErrors _log;
    private ICurrentUserService _user;


    public UserService(
        AplicationDbContext context,
        IMapper mapper,
        ILogErrors log
,
        ICurrentUserService user)
    {
        _context = context;
        _mapper = mapper;
        _log = log;
        _user = user;
    }


    public async Task<int> Create(UsuariosDto req, CancellationToken cancellation = default)
    {
        try
        {
            var user = new SegUsuarios
            {
                Nombre = req.Nombre,
                IdcEstadousaurio = 21,
                Login = req.Login,
                Cedula = req.Cedula,
                IdsegRoles = req.IdsegRoles,
                Password = CGeneric.Md5Hash(req.Password)
            };

            await _context.AddAsync(user, cancellation);
            var resp = await _context.SaveChangesAsync(cancellation);
        }
        catch (Exception e)
        {
            _log.Register(e);
        }

        return req.IdsegUsuarios;
    }

    public async Task<int> Delete(int id, CancellationToken cancellation = default)
    {
        try
        {
            var ent = _context.Usuarios.Find(id);
            _context.Usuarios.Remove(ent);
            var resp = await _context.SaveChangesAsync(cancellation);
        }
        catch (Exception e)
        {
            _log.Register(e);
            id = 0;
        }

        return id;
    }

    public List<SegUsuarios> GetAll()
    {
        var vlist = new List<SegUsuarios>();
        try
        {
            vlist = _context.Usuarios.ToList().OrderByDescending(x => x.IdsegUsuarios).ToList();
        }
        catch (Exception e)
        {
            _log.Register(e);
        }
        return vlist;
    }

    public SegUsuarios GetById(int id)
    {
        var entidad = new SegUsuarios();
        try
        {
            entidad = _context.Usuarios.Find(id);
        }
        catch (Exception e)
        {
            _log.Register(e);
        }

        return entidad;
    }

    public async Task<int> Update(UsuariosDto req, CancellationToken cancellation = default)
    {
        try
        {

            var user = await _context.Usuarios.FindAsync(req.IdsegUsuarios);
            
            user.Nombre = req.Nombre;
            user.IdcEstadousaurio = req.IdcEstadousaurio;
            user.Login = req.Login;
            user.Cedula = req.Cedula;
            user.IdsegRoles = req.IdsegRoles;
            user.Password = CGeneric.Md5Hash(req.Password);

            _context.Update(user);
            var resp = await _context.SaveChangesAsync(cancellation);

        }
        catch (Exception e)
        {
            _log.Register(e);
        }

        return req.IdsegUsuarios;
    }

    public List<SegRoles> GetAllRoles()
    {
        var vlist = new List<SegRoles>();
        try
        {
            vlist = _context.Roles.ToList();
        }
        catch (Exception e)
        {
            _log.Register(e);
        }
        return vlist;
    }

    public async Task<ResponseEntity> Login(AuthModel requeest)
    {
        var respuesta = new ResponseEntity();
        try
        {
            var user = await _context.Usuarios
                        .Where(b => b.Login == requeest.UserName && b.Password.ToUpper() == CGeneric.Md5Hash(requeest.Password))
                        .Include(c => c.Roles).FirstAsync();

            respuesta.Data = user != null ? user : null;
            respuesta.State = user != null ? State.Success : State.NoData;
            respuesta.Message = user != null ? "" : null;
        }
        catch (Exception e)
        {
            _log.Register(e);
            respuesta.State = State.Error;
            respuesta.Message = e.Message;
        }

        return respuesta;
    }

    public async Task<List<ParamClasificador>> GetEstados()
    {
        var vList = new List<ParamClasificador>();
        try
        {
            vList = await _context.Clasificador
                        .Where(b => b.IdparamClasificadortipo ==  6).ToListAsync();
        }
        catch (Exception e)
        {
            _log.Register(e);
           
        }
        return vList;
    }
}