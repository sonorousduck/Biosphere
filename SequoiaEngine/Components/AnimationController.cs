using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequoiaEngine
{
    public class Link
    {
        public int ChildNode;
        public Func<bool> ShouldTraverseLink;

        public Link(int childNode, Func<bool> shouldTraverse) 
        {
            this.ChildNode = childNode;
            this.ShouldTraverseLink = shouldTraverse;
        }
    }

    public class Node
    {
        public List<Link> Links;
        public List<AnimatedSprite> Animations;
        public Node(List<Link> links, List<AnimatedSprite> animations)
        {
            this.Links = links;
            this.Animations = animations;
        }
    }

    public class AnimationTree
    {
        public List<Node> Tree = new();
        
        public void AddNode(Node node) 
        {
            Tree.Add(node);
        }
    }


    public class AnimationController : RenderedComponent
    {
        public int CurrentNode = 0;
        public AnimationTree AnimationTree;
        public float RenderDepth;

        public AnimationController(float renderDepth)
        {
            this.RenderDepth = renderDepth;
            this.AnimationTree = new();
        }

        public AnimationController(AnimationTree animationTree, float renderDepth)
        {
            this.AnimationTree = animationTree;
            this.RenderDepth = renderDepth;
        }



        public List<AnimatedSprite> GetSprites()
        {
            return AnimationTree.Tree[CurrentNode].Animations;
        }
    }
}
