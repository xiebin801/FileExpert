﻿using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileExpert.MP4.Box
{
    class MetaBox : FullBox
    {
        public MetaBox(TreeView treeView, TreeNode parent, DataStore dataStore, ref Int64 bitOffset) :
            base(treeView, parent, dataStore, ref bitOffset)
        {

            TreeNode nodeBoxes = null;

            Int64 firstOffset = bitOffset;

            //Add one node to indicate this box.
            Result result = Utility.AddNodeContainer(Position.CHILD, parent, out nodeBoxes, "MetaBox_payload", ItemType.SECTION1, dataStore, bitOffset, dataStore.GetLeftBitLength());

            ParseMp4 parserMp4 = new ParseMp4();

            parserMp4.ParseInnerBox(treeView, nodeBoxes, dataStore, ref bitOffset);

            if (result.Fine)
            {
                Utility.UpdateNodeLength(nodeBoxes, "MetaBox_payload", ItemType.SECTION1, bitOffset - firstOffset);
            }
        }
    }
}
