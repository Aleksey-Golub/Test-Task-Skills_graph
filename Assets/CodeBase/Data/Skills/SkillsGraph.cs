using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.SkillsData;

namespace CodeBase.Data.Skills
{
    public class SkillsGraph
    {
        private const SkillId BASE_SKILL = SkillId.SkillBase;
        
        private readonly Dictionary<SkillId, List<SkillId>> _adjacency;
        private readonly HashSet<SkillId> _openSkills;

        public SkillsGraph(SkillTreeConfig config)
        {
            _adjacency = new Dictionary<SkillId, List<SkillId>>();
            _openSkills = new HashSet<SkillId>();

            for (int i = 0; i < config.SkillGraph.Count; i++)
            {
                SkillConfig skill = config.SkillGraph[i];
                _adjacency[skill.Id] = new List<SkillId>();
                _adjacency[skill.Id].AddRange(skill.LinkedSkills);
            }

            _openSkills.Add(BASE_SKILL);
        }

        public bool IsOpen(SkillId skillId) => _openSkills.Contains(skillId);

        public bool CanOpen(SkillId skillId)
        {
            if (IsOpen(skillId)) 
                return false;
            
            return _adjacency[skillId].Any(n => IsOpen(n));
        }

        public bool Open(SkillId skillId)
        {
            if (CanOpen(skillId))
            {
                _openSkills.Add(skillId);
                return true;
            }

            return false;
        }

        public bool CanClose(SkillId skillId)
        {
            if (skillId is BASE_SKILL) 
                return false;
            
            if (!IsOpen(skillId)) 
                return false;
            
            var tempOpen = new HashSet<SkillId>(_openSkills);
            tempOpen.Remove(skillId);

            return IsConnected(tempOpen, BASE_SKILL);
        }

        public bool Close(SkillId skillId)
        {
            if (CanClose(skillId))
            {
                _openSkills.Remove(skillId);
                return true;
            }

            return false;
        }

        public bool IsLinked()
        {
            var allOpened = _adjacency.Select(kvp => kvp.Key).ToHashSet();
            return IsConnected(allOpened, BASE_SKILL);
        }
        
        private bool IsConnected(HashSet<SkillId> openSkills, SkillId start)
        {
            if (!openSkills.Contains(start)) 
                return false;

            var visited = new HashSet<SkillId>();
            var queue = new Queue<SkillId>();
            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                SkillId current = queue.Dequeue();
                foreach (SkillId neighbor in _adjacency[current])
                {
                    if (openSkills.Contains(neighbor) && visited.Add(neighbor))
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return openSkills.All(n => visited.Contains(n));
        }

        public override string ToString()
        {
            return $"Open nodes: {string.Join(", ", _openSkills.OrderBy(x => x))}";
        }
    }
}