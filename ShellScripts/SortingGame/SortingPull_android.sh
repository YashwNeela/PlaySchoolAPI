cd Assets
cd Colorful-Crayons
git reset --hard main
git config core.sparseCheckout true
echo Assets/_SortingGame/_Scripts/ > .git\info\sparse-checkout
git fetch origin
git pull origin main
git read-tree -mu HEAD

