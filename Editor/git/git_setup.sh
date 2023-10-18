#! /bin/bash

set -eo pipefail

start_directory="$PWD"

# Flags
USE_LFS=false
REMOTE_URL=""

while getopts lr: OPTIONS; do
  case $OPTIONS in
    l)
      USE_LFS=true
      ;;
    r)
      REMOTE_URL=$OPTARG
      ;;
    *)
      ;;
  esac
done

# Switch to root of project
# You have to go back 4 times for the tests to work and
# 5 times for it to work as an imported package from git
if [[ $start_directory == *"Library"* ]]; then
  cd ../../../../..
elif [[ $start_directory == *"Packages"* ]]; then
  cd ../../../..
else
  echo "Package is not inside project. Cannot run git setup."
  exit 1
fi

# Initialize Repository
echo "Initializing git repository"
git init
cp "$start_directory"/gitignore .
mv ./gitignore ./.gitignore

# Set up git LFS
if [ "$USE_LFS" = true ]; then
  echo "Initializing git lfs"
  git lfs install
  cp "$start_directory"/.gitattributes .
fi

# Make initial commit

echo "Creating initial commit"
git add .
git commit -m "Initial commit"

# Link repository to remote
if [ -n "$REMOTE_URL" ]; then
  echo "Linking to remote repository"
  git remote add origin "$REMOTE_URL"
  git push -u origin main
fi

# Set up git hooks
#echo "Adding git hooks"
#chmod +x "$start_directory"/commit-msg.sh
#chmod +x "$start_directory"/update.sh
#cp "$start_directory"/commit-msg.sh ./.git/hooks
#cp "$start_directory"/update.sh ./.git/hooks
#mv ./.git/hooks/commit-msg.sh ./.git/hooks/commit-msg
#mv ./.git/hooks/update.sh ./.git/hooks/update
