#!/bin/bash

OLD_EMAIL="zarr08060@gmail.com"
CORRECT_NAME="Reza Zare"
CORRECT_EMAIL="rezasingle.org@gmail.com"

git filter-branch --env-filter "
if [ \"\$GIT_COMMITTER_EMAIL\" = \"$OLD_EMAIL\" ]; then
    export GIT_COMMITTER_NAME=\"$CORRECT_NAME\"
    export GIT_COMMITTER_EMAIL=\"$CORRECT_EMAIL\"
fi
if [ \"\$GIT_AUTHOR_EMAIL\" = \"$OLD_EMAIL\" ]; then
    export GIT_AUTHOR_NAME=\"$CORRECT_NAME\"
    export GIT_AUTHOR_EMAIL=\"$CORRECT_EMAIL\"
fi
" --tag-name-filter cat -- --branches --tags
