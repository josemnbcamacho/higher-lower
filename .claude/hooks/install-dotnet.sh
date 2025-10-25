#!/bin/bash
# install-dotnet.sh - Installs .NET SDK for Higher-Lower game

# Check if dotnet is already installed
if command -v dotnet &> /dev/null; then
    echo "✓ .NET SDK already installed (version: $(dotnet --version))"
    exit 0
fi

echo "Installing .NET SDK..."

# Download and execute the official .NET install script
curl -fsSL https://dot.net/v1/dotnet-install.sh | bash

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

