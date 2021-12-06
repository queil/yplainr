#!/bin/sh
PKG=$1
: ${OUT_DIR:="./src/import"}
yarn add $PKG --dev
PKG_LOCAL_PATH=$(yarn unplug $PKG | grep 'Will unpack .* to')
PKG_SHORT=${PKG##*/}
TS_DEF_PATH="${PKG_LOCAL_PATH##*to }/node_modules/@types/$PKG_SHORT/index.d.ts"
FS_FILE_NAME=$(echo -n "$PKG_SHORT" | sed -r 's/(^|-)([a-z])/\U\2/g')
FS_OUTPUT_PATH="$OUT_DIR/Fable.Import.$FS_FILE_NAME.fs"
echo -n "$TS_DEF_PATH --> $FS_OUTPUT_PATH"

yarn dlx ts2fable $TS_DEF_PATH $FS_OUTPUT_PATH
