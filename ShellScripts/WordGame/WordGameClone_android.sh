cd Assets
git clone --recurse-submodules https://github.com/TMKOC-Play-School/WordGame
cd WordGame1/WordGame
git config core.sparseCheckout true
echo Assets/Scripts/ >> .git\info\sparse-checkout
git read-tree -mu HEAD

