cd Assets
cd WordGame
git reset --hard main
git config core.sparseCheckout true
echo Assets/Scripts/ > .git/info/sparse-checkout
git fetch origin
git pull origin main
git read-tree -mu HEAD

