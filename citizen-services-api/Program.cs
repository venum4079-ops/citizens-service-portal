using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ServicesDb>(options => options.UseSqlite("Data Source=services.db"));
builder.Services.AddCors(options => options.AddPolicy("local", policy => policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod()));
var app = builder.Build();
app.UseCors("local");
using (var scope = app.Services.CreateScope()) {
 var db = scope.ServiceProvider.GetRequiredService<ServicesDb>();
 db.Database.EnsureCreated();
 if (!db.Requests.Any()) { db.Requests.AddRange(new ServiceRequest { ApplicantName="Alex Morgan", Program="Housing Assistance", Status="In Review", SubmittedAt=DateTime.UtcNow.AddDays(-3) }, new ServiceRequest { ApplicantName="Jordan Lee", Program="Food Assistance", Status="Approved", SubmittedAt=DateTime.UtcNow.AddDays(-7) }); db.SaveChanges(); }
}
app.MapGet("/api/requests", async (ServicesDb db, string? status) => await db.Requests.Where(r => status == null || r.Status == status).OrderByDescending(r => r.SubmittedAt).ToListAsync());
app.MapGet("/api/requests/{id:int}", async (int id, ServicesDb db) => await db.Requests.FindAsync(id) is { } item ? Results.Ok(item) : Results.NotFound());
app.MapPost("/api/requests", async (CreateRequest input, ServicesDb db) => {
 var errors = new List<ValidationResult>();
 if (!Validator.TryValidateObject(input, new ValidationContext(input), errors, true)) return Results.ValidationProblem(errors.GroupBy(e => e.MemberNames.FirstOrDefault() ?? "request").ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage ?? "Invalid").ToArray()));
 var item = new ServiceRequest { ApplicantName=input.ApplicantName.Trim(), Program=input.Program.Trim(), Status="Submitted", SubmittedAt=DateTime.UtcNow };
 db.Requests.Add(item); await db.SaveChangesAsync(); return Results.Created($"/api/requests/{item.Id}", item);
});
app.MapPatch("/api/requests/{id:int}/status", async (int id, UpdateStatus input, ServicesDb db) => {
 var item=await db.Requests.FindAsync(id); if(item is null) return Results.NotFound();
 if (!new[]{"Submitted","In Review","Approved","Denied"}.Contains(input.Status)) return Results.BadRequest(new { error="Invalid status" });
 item.Status=input.Status; await db.SaveChangesAsync(); return Results.Ok(item);
});
app.MapDelete("/api/requests/{id:int}", async (int id, ServicesDb db) => { var item=await db.Requests.FindAsync(id); if(item is null) return Results.NotFound(); db.Requests.Remove(item); await db.SaveChangesAsync(); return Results.NoContent(); });
app.Run();
public class ServicesDb(DbContextOptions<ServicesDb> options):DbContext(options) { public DbSet<ServiceRequest> Requests => Set<ServiceRequest>(); }
public class ServiceRequest { public int Id {get;set;} public string ApplicantName {get;set;}=""; public string Program {get;set;}=""; public string Status {get;set;}="Submitted"; public DateTime SubmittedAt {get;set;} }
public record CreateRequest([property:Required, StringLength(100,MinimumLength=2)] string ApplicantName, [property:Required, StringLength(80,MinimumLength=2)] string Program);
public record UpdateStatus(string Status);
