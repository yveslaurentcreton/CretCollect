# CretCollect Development Container
# Base: .NET 9.0 on Debian Bookworm
FROM mcr.microsoft.com/devcontainers/dotnet:1-9.0-bookworm

# Install system dependencies for debugging
# Note: Remove yarn repo if it exists (base image has expired key issue)
RUN rm -f /etc/apt/sources.list.d/yarn.list 2>/dev/null || true \
    && apt-get update && apt-get install -y --no-install-recommends \
    curl \
    unzip \
    procps \
    && rm -rf /var/lib/apt/lists/*

# Install netcoredbg (Samsung's command-line .NET debugger)
# This enables CLI-based debugging with breakpoints, stepping, variable inspection
ARG NETCOREDBG_VERSION=3.1.3-1062
RUN curl -sSL "https://github.com/Samsung/netcoredbg/releases/download/${NETCOREDBG_VERSION}/netcoredbg-linux-amd64.tar.gz" \
    -o /tmp/netcoredbg.tar.gz \
    && mkdir -p /usr/local/netcoredbg \
    && tar -xzf /tmp/netcoredbg.tar.gz -C /usr/local/netcoredbg \
    && ln -s /usr/local/netcoredbg/netcoredbg/netcoredbg /usr/local/bin/netcoredbg \
    && rm /tmp/netcoredbg.tar.gz

# Install .NET Diagnostic Tools globally
# These run as the vscode user, so we install them in the user context
USER vscode
RUN dotnet tool install -g dotnet-dump \
    && dotnet tool install -g dotnet-trace \
    && dotnet tool install -g dotnet-counters \
    && dotnet tool install -g dotnet-gcdump \
    && dotnet tool install -g dotnet-stack

# Ensure .NET tools are in PATH
ENV PATH="${PATH}:/home/vscode/.dotnet/tools"

USER root
