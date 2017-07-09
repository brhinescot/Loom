#region Using Directives

using System;
using System.Web.UI;

#endregion

namespace Loom.Web.Tests
{
    public partial class ManagementConsoleContent : Page
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            string param = Page.Request.QueryString["content"];

            switch (param)
            {
                case "Planes":
                {
                    content.Text = @"A fixed-wing aircraft is a heavier-than-air craft whose lift is generated not by wing motion relative to the aircraft, but by forward motion through the air. The term is used to distinguish from rotary-wing aircraft or ornithopters, where the movement of the wing surfaces relative to the aircraft generates lift. In the United States and Canada, the term airplane is used; in the rest of the English-speaking countries (including Ireland and Commonwealth nations), the term aeroplane is more common. These terms refer to any fixed wing aircraft powered by propellers or jet engines. The word derives from the Greek αέρας (aéras-) (""air"") and -plane.[1] The spelling ""aeroplane"" is the older of the two, dating back to the mid-late 19th century.[2] Some fixed-wing aircraft may be remotely or robot controlled.";
                    break;
                }
                case "Trains":
                {
                    content.Text = @"A train is a connected series of vehicles that move along a track (permanent way) to transport freight or passengers from one place to another. The track usually consists of two rails, but might also be a monorail or maglev guideway. Propulsion for the train is provided by a separate locomotive, or from individual motors in self-propelled multiple units. Most modern trains are powered by diesel locomotives or by electricity supplied by overhead wires or additional rails, although historically (from the early 19th century to the mid-20th century) the steam locomotive was the dominant form of locomotive power. Other sources of power (such as horses, rope or wire, gravity, pneumatics, and gas turbines) are possible.";
                    break;
                }
                case "Cats":
                {
                    content.Text = @"The cat (Felis catus), also known as the domestic cat or house cat to distinguish it from other felines, is a small predatory carnivorous species of crepuscular mammal that is valued by humans for its companionship and its ability to hunt vermin, snakes and scorpions. It has been associated with humans for at least 9,500 years.[5]

A skilled predator, the cat is known to hunt over 1,000 species for food. It can be trained to obey simple commands. Individual cats have also been known to learn on their own to manipulate simple mechanisms, such as doorknobs. Cats use a variety of vocalizations and types of body language for communication, including meowing, purring, hissing, growling, squeaking, chirping, clicking, and grunting.[6] Cats may be the most popular pet in the world, with over 600 million in homes all over the world.[7] They are also bred and shown as registered pedigree pets. This hobby is known as the ""cat fancy"".";
                    break;
                }
                default:
                {
                    content.Text = "Comming Sooon";
                    break;
                }
            }
        }
    }
}