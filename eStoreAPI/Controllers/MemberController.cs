using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess.Repository;

namespace eStoreAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IConfiguration Configuration;

        public class LoginInformation
        {
            public string email { get; set; }
            public string password { get; set; }
        }

        public MemberController(IMemberRepository memberRepository, IConfiguration configuration)
        {
            _memberRepository = memberRepository;
            Configuration = configuration;
        }

        [HttpPost("authentication")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<Member>> Authentication(LoginInformation loginInformation)
        {
            Member member = await _memberRepository.Authentication(loginInformation.email, loginInformation.password);
            if (member == null)
            {
                //if get member from database is null, check if it was the admin

                if (loginInformation.email.Equals(Configuration["Admin:Email"], StringComparison.OrdinalIgnoreCase) && loginInformation.password.Equals(Configuration["Admin:Password"]))
                {
                    return new Member { Email = Configuration["Admin:Email"], Password = Configuration["Admin:Password"], isAdmin = true};
                }

                return NotFound();
            }

            return Ok(member);
        }

        // [HttpGet("is-admin/{id}")]
        // public async Task<bool> isAdmin(int id)
        // {
        //     
        // }

        // GET: api/Member
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Member>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            var list = await _memberRepository.GetAll();
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        // GET: api/Member/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Member))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            Member? member = await _memberRepository.Get(id);
            // if (_context.Members == null)
            // {
            //     return NotFound();
            // }

            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        // PUT: api/Member/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, Member member)
        {
            if (id != member.MemberId)
            {
                return BadRequest();
            }

            // _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _memberRepository.Update(member);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _memberRepository.Get(member.MemberId) == null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Member
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            // if (_context.Members == null)
            // {
            //     return Problem("Entity set 'FStoreDBContext.Members'  is null.");
            // }
            try
            {
                await _memberRepository.Add(member);
            }
            catch (DbUpdateException)
            {
                if (await _memberRepository.Get(member.MemberId) != null)
                {
                    return Conflict();
                }

                throw;
            }

            return CreatedAtAction("GetMember", new { id = member.MemberId }, member);
        }

        // DELETE: api/Member/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            // if (_context.Members == null)
            // {
            //     return NotFound();
            // }

            var member = await _memberRepository.Get(id);
            if (member == null)
            {
                return NotFound();
            }

            await _memberRepository.Delete(id);
            return NoContent();
        }

        // private bool MemberExists(int id)
        // {
        //     return (_context.Members?.Any(e => e.MemberId == id)).GetValueOrDefault();
        // }
    }
}