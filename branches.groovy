library identifier: 'build-configs@master',

        retriever: modernSCM([$class: 'GitSCMSource', credentialsId: '7d49a174-7aba-4ac4-88e6-489ab13a4156',
                              remote: 'git@github.com:CMHWorks/build-configs.git',
                              traits: [[$class: 'WipeWorkspaceTrait']]])

build("answer_branches")