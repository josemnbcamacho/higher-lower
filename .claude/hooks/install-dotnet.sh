#!/bin/bash
# install-dotnet.sh - Installs .NET SDK for Higher-Lower game

# Check if dotnet is already installed
if command -v dotnet &> /dev/null; then
    echo "✓ .NET SDK already installed (version: $(dotnet --version))"
    exit 0
fi

echo "Installing .NET SDK..."

# Download and execute the official .NET install script
# Pass --version with a specific version to COMPLETELY bypass aka.ms (which is blocked)
# When version is not "latest", the script skips aka.ms entirely and uses direct feeds only
# Using a recent 9.0.x version - adjust if you need a different version
if ! curl -fsSL -L https://dot.net/v1/dotnet-install.sh | bash -s -- --version 9.0.111; then
    echo "✗ Failed to download .NET install script"
    echo "This may be due to network restrictions in the sandboxed environment"
    exit 1
fi

# Add .NET to the PATH for the current session
export DOTNET_ROOT=$HOME/.dotnet
export PATH=$DOTNET_ROOT:$PATH 

# Verify installation
if command -v dotnet &> /dev/null; then
    echo "✓ .NET SDK installed successfully (version: $(dotnet --version))"
else
    echo "✗ Failed to install .NET SDK"
    exit 1
fi