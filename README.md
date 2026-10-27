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

## Project 2 — State Program Dashboard
- React, TypeScript, Vite, CSS
- Interactive category filtering, text search, sorting, KPI calculations, progress bars, responsive layout
- Uses synthetic, in-memory sample data; no backend required

### Run
1. In `state-program-dashboard`, run `npm install` then `npm run dev`.
2. Open the URL Vite prints (typically `http://localhost:5173`).

## Verify before submission
- Run both projects and capture real screenshots for the repository.
- Review, understand, and customize the code; ensure you can explain design choices in interviews.
- Add your own GitHub URLs to your resume once published.
- Do not claim these were built for a past employer or that the sample data is official.

## Production considerations
These are portfolio demonstrations, **not production-ready government applications**. Before production use, add authentication/authorization, privacy controls, audit logging, integration tests, CI/CD, secure configuration, API documentation, pagination, rate limits, and a supported production database. SQLite was chosen for local reproducibility; SQL Server can be configured with the appropriate EF Core provider.
