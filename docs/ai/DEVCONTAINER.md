# Devcontainer Instructions for AI Agents

This document provides instructions for AI agents working with the CretCollect devcontainer.

## Starting the Devcontainer

### Via VS Code (recommended for humans)
1. Open the repository in VS Code
2. Click "Reopen in Container" when prompted
3. Wait for the container to build and start

### Via CLI (for AI agents without VS Code)
```bash
# Using devcontainer CLI
devcontainer up --workspace-folder /path/to/cretcollect

# Or using Docker directly
cd /path/to/cretcollect
docker build -t cretcollect-dev -f .devcontainer/Dockerfile .devcontainer/
docker run -it --rm \
  --cap-add=SYS_PTRACE \
  --security-opt seccomp=unconfined \
  -v $(pwd):/workspace \
  -w /workspace \
  cretcollect-dev bash
```

**Important:** The `--cap-add=SYS_PTRACE` and `--security-opt seccomp=unconfined` flags are required for debugging!

## Running the Application

### Option 1: Using Docker Compose (full stack)
```bash
cd docker
docker-compose up -d
```

This starts:
- **Keycloak** on port 26000 (admin/admin)
- **PostgreSQL** on port 5432
- **API** on port 5108/7249
- **App** on port 5045/7232

### Option 2: Using Aspire (development)
```bash
cd src
dotnet run --project CretCollect.AppHost
```

### Test Users (Keycloak)
- `admin` / `admin`
- `developer` / `developer`

## Debugging Tools

### netcoredbg (Interactive CLI Debugger)
```bash
# Build in Debug mode first
dotnet build -c Debug

# Start app in background
dotnet ./bin/Debug/net9.0/YourApp.dll &
APP_PID=$!

# Attach debugger
netcoredbg --interpreter=cli --attach $APP_PID

# Commands:
# break Program.cs:42  - Set breakpoint
# continue            - Continue execution
# next                - Step over
# step                - Step into
# print varName       - Print variable
# backtrace           - Show call stack
# quit                - Exit
```

### Diagnostic Tools
```bash
# Find .NET processes
dotnet-dump ps

# Collect crash dump
dotnet-dump collect -p <pid>

# Live metrics
dotnet-counters monitor -p <pid>

# Performance tracing
dotnet-trace collect -p <pid> --duration 00:00:30

# GC heap analysis
dotnet-gcdump collect -p <pid>
```

## Project Structure

```
src/
├── CretCollect.AppHost/      # Aspire orchestration
├── CretCollect.App.Server/   # Blazor Server host
├── CretCollect.App.Wasm/     # Blazor WebAssembly client
└── CretCollect.sln           # Solution file

docker/
├── docker-compose.yaml       # Full stack (Keycloak + PostgreSQL + API + App)
├── api/Dockerfile
└── app/Dockerfile
```

## Authentication

CretCollect uses **Keycloak** for authentication (OpenID Connect).

Configuration is in:
- `src/CretCollect.AppHost/realms/cretcollect-import.json` - Keycloak realm config
- `src/CretCollect.App.Server/appsettings.json` - App security settings

## Tips for AI Agents

1. **Always build in Debug mode** for debugging: `dotnet build -c Debug`
2. **Use Docker Compose** for local development with full auth stack
3. **Check Keycloak** at http://localhost:26000 if auth issues occur
4. **Use full paths** for diagnostic tools when PATH isn't set
5. **The container must have SYS_PTRACE** for any debugger to work
