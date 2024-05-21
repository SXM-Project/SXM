fx_version 'cerulean'
game 'gta5'
-- rdr3_warning 'I acknowledge that this is a prerelease build of RedM, and I am aware my resources *will* become incompatible once RedM ships.'

author 'bub.bl'
description 'An awesome, but short, description'
version '1.0.0'

client_scripts {
    "build/client/sxm-client.net.dll"
}

server_scripts {
    "build/server/netstandard2.1/sxm-server.net.dll"
}

files {
    "build/deps/**/*.dll",
    "build/client/**/*.dll",
    "build/server/**/*.dll",
    "build/client/ui/wwwroot/*",
}

-- Prod Mode
-- ui_page "build/client/ui/wwwroot/index.html"

-- Dev Mode
ui_page "https://localhost:7219"

escrow_ignore {
    ".git/*";
    "code/*",
    "build/deps/*",
    "Makefile.toml",
    "README.md",
    ".gitattributes",
    ".gitignore"
}

-- dependency "webpack"
-- dependency "yarn"

-- webpack_config "client.config.js"

-- client_scripts {
--     "build/client/index.js"
-- }

-- server_scripts {
--     "build/server/index.js"
-- }
