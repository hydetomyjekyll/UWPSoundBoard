using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPSoundBoard.Model
{
    public class SoundManager
    {

        /// <summary>
        /// This method returns all the sounds available in the list
        /// </summary>
        /// <param name="sounds">The passed observable collection which is updated with the desired value</param>
        public static void GetAllSounds(ObservableCollection<Sound> sounds)
        {
            var allSounds = GetSounds();
            sounds.Clear();
            allSounds.ForEach(p => sounds.Add(p));
        }



        /// <summary>
        /// This method returns the sound which is filtered by category passed as parameter
        /// </summary>
        /// <param name="sounds">The passed observable collection which is updated with the desired value</param>
        /// <param name="soundCategory">The category which we want to filter</param>
        public static void GetSoundsByCategory(ObservableCollection<Sound> sounds, SoundCategory soundCategory)
        {
            var allSounds = GetSounds();
            var filteredSounds = allSounds.Where(p => p.Category == soundCategory).ToList();
            sounds.Clear();
            filteredSounds.ForEach(p => sounds.Add(p));
        }



        public static void GetSoundsByName(ObservableCollection<Sound> sounds, String header, String beginningStr)
        {
            var allSounds = GetSounds();

            switch (header)
            {
                case "All Sounds":
                    break;

                case "Animal Sounds":
                    allSounds = allSounds.Where(p => p.Category == SoundCategory.Animals).ToList();
                    break;

                case "Cartoon Sounds":
                    allSounds = allSounds.Where(p => p.Category == SoundCategory.Cartoons).ToList();
                    break;

                case "Taunt Sounds":
                    allSounds = allSounds.Where(p => p.Category == SoundCategory.Taunts).ToList();
                    break;

                case "Warning Sounds":
                    allSounds = allSounds.Where(p => p.Category == SoundCategory.Warnings).ToList();
                    break;
            }

            var filteredSounds = allSounds.Where(p => p.Name.ToLower().StartsWith(beginningStr.ToLower())).ToList();

            if(filteredSounds.Count() != sounds.Count())
            {
                sounds.Clear();
                filteredSounds.ForEach(p => sounds.Add(p));
            }
            
            
        }


        /// <summary>
        /// Method that returns all the available elements in the assets
        /// </summary>
        /// <returns></returns>
        private static List<Sound> GetSounds()
        {
            var sounds = new List<Sound>();

            sounds.Add(new Sound("Cow", SoundCategory.Animals));
            sounds.Add(new Sound("Cat", SoundCategory.Animals));

            sounds.Add(new Sound("Gun", SoundCategory.Cartoons));
            sounds.Add(new Sound("Spring", SoundCategory.Cartoons));

            sounds.Add(new Sound("Clock", SoundCategory.Taunts));
            sounds.Add(new Sound("LOL", SoundCategory.Taunts));

            sounds.Add(new Sound("Ship", SoundCategory.Warnings));
            sounds.Add(new Sound("Siren", SoundCategory.Warnings));

            return sounds;
        }
    }
}
