cd Assets
git clone --recurse-submodules https://github.com/SATYAM-NF/SpotDifference-2D
cd SpotDifference-2D
git config core.sparseCheckout true
echo Assets/IN-GAME/Scripts/ >> .git\info\sparse-checkout
git read-tree -mu HEAD

