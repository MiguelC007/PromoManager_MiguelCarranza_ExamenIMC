using Microsoft.AspNetCore.Mvc;
using PromoManager.Application.Services;
using PromoManager.Application.Requests;

namespace PromoManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CouponsController : ControllerBase {
    private readonly CouponService _svc;
    public CouponsController(CouponService svc) => _svc = svc;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCouponRequest req) {
        try {
            var dto = await _svc.CreateAsync(req);
            return CreatedAtAction(nameof(GetByCode), new { code = dto.Code }, dto);
        } catch (Exception ex) {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> List() => Ok(await _svc.ListAsync());

    [HttpGet("{code}")]
    public async Task<IActionResult> GetByCode(string code) {
        var dto = await _svc.GetByCodeAsync(code);
        return dto == null ? NotFound() : Ok(dto);
    }

    [HttpPost("{code}/deactivate")]
    public async Task<IActionResult> Deactivate(string code) {
        var (ok, reason) = await _svc.DeactivateAsync(code);
        if (!ok) return BadRequest(new { reason });
        return Ok();
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete(string code) {
        var ok = await _svc.DeleteAsync(code);
        return ok ? NoContent() : NotFound();
    }

    [HttpGet("{code}/validate")]
    public async Task<IActionResult> Validate(string code) {
        var (valid, reason) = await _svc.ValidateAsync(code, DateTime.UtcNow);
        return Ok(new { valid, reason });
    }
}