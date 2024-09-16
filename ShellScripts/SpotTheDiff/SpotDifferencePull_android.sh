cd Assets
cd SpotDifference-2D
git reset --hard main
git config core.sparseCheckout true
echo Assets/IN-GAME/Scripts/ > .git\info\sparse-checkout
git fetch origin
git pull origin main
git read-tree -mu HEAD

