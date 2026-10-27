[README.md](https://github.com/user-attachments/files/34026207/README.md)
# Senior .NET Full Stack + UI/UX Portfolio Samples

Two original, fictional public-sector-inspired demonstration projects. **These are newly created samples, not prior employment work, and should be described as such.** No real citizen data or Iowa government systems are used.

## Project 1 — Citizen Services Portal
- ASP.NET Core 8 minimal API, EF Core, SQLite (easy local setup), React, TypeScript, Vite
- Request creation, listing, filtering, status updates, deletion endpoint, validation, seeded synthetic records
- Responsive operations dashboard with labeled controls and accessible error feedback

### Run
1. Install .NET 8 SDK and Node.js 20+.
2. In `citizen-services-api`, run `dotnet restore` then `dotnet run --urls http://localhost:5000`.
3. In a separate terminal, in `citizen-services-ui`, run `npm install` then `npm run dev`.
4. Open `http://localhost:5173`.

### API routes
`GET /api/requests`, `GET /api/requests/{id}`, `POST /api/requests`, `PATCH /api/requests/{id}/status`, `DELETE /api/requests/{id}`.

Example POST JSON: `{ "applicantName": "Taylor Smith", "program": "Housing Assistance" }`.
duction database. SQLite was chosen for local reproducibility; SQL Server can be configured with the appropriate EF Core provider.
