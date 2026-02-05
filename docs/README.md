# CretCollect Documentation

## Structure

- `ai/` - Instructions specifically for AI agents working on this codebase
  - `DEVCONTAINER.md` - How to use the devcontainer and debugging tools

## For AI Agents

If you're an AI agent (Copilot, Claude, etc.) working on this codebase:

1. **Read `ai/DEVCONTAINER.md`** first if you need to debug or run the application
2. **Use the devcontainer** for a consistent development environment
3. **Use Docker Compose** for the full stack with Keycloak authentication

## For Humans

The project uses a devcontainer for development. Open in VS Code and click "Reopen in Container".

To run the full stack locally:
```bash
cd docker
docker-compose up -d
```

Then access:
- App: http://localhost:5045
- API: http://localhost:5108
- Keycloak: http://localhost:26000 (admin/admin)
